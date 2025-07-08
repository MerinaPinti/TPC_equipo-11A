using Dominio;
using Negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Clinica
{
    public partial class HorarioMedico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"] != null ? (Usuario)Session["usuario"] : null;
            if (usuario == null || usuario.TipoUsuario.Id == 2)
            {
                Session["error"] = "No tiene permisos para acceder a esta página.";
                Response.Redirect("Error.aspx", true);
            }
            Session["paginaAnterior"] = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

            if (!IsPostBack)
            {
                cargarDropDowns();
                cargarGridHorario();
                controlarFormularioModificacion();
            }
        }

        private void cargarDropDowns()
        {
            List<string> dias = new List<string> { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };
            ddlDia.DataSource = dias;
            ddlDia.DataBind();

            List<string> horarios = new List<string>();
            for (int i = 0; i < 24; i++)
            {
                horarios.Add(i.ToString("D2") + ":00");
            }

            ddlHoraInicio.DataSource = horarios;
            ddlHoraInicio.DataBind();
            horarios.Remove(horarios[0]);
            ddlHoraFin.DataSource = horarios;
            ddlHoraFin.DataBind();

            Usuario usuario = (Usuario)Session["usuario"];
            MedicoNegocio negocio = new MedicoNegocio();
            Medico medicoLogueado = (Medico)Session["medico"];
            ddlEspecialidad.DataSource = medicoLogueado.Especialidad;
            ddlEspecialidad.DataTextField = "Descripcion";
            ddlEspecialidad.DataValueField = "Id";
            ddlEspecialidad.DataBind();
        }

        private void cargarDropDowns(int horarioInicio)
        {

            List<string> horarios = new List<string>();
            for (int i = horarioInicio + 1; i < 24; i++)
            {
                horarios.Add(i.ToString("D2") + ":00");
            }
            ddlHoraFin.DataSource = horarios;
            ddlHoraFin.DataBind();
        }

        private void cargarGridHorario()
        {
            HorarioAtencionNegocio horarioAtencionNegocio = new HorarioAtencionNegocio();
            Medico medicoLogueado = (Medico)Session["medico"];
            gvHorarioMedico.DataSource = horarioAtencionNegocio.listarConIdMedico(medicoLogueado.IdMedico);
            gvHorarioMedico.DataBind();
        }

        private void controlarFormularioModificacion()
        {
            if (Session["idModificarHorario"] != null)
            {
                int id = (int)Session["idModificarHorario"];
                HorarioAtencionNegocio horarioNegocio = new HorarioAtencionNegocio();
                HorarioAtencion horario = horarioNegocio.listar(id);

                btnCancelar.Visible = true;
                gvHorarioMedico.Visible = false;
                alert.Visible = false;
                ddlDia.SelectedIndex = horario.DiaSemana - 1;
                ddlEspecialidad.SelectedValue = horario.Especialidad.Id.ToString();
                ddlHoraInicio.SelectedValue = horario.HorarioInicio.ToString(@"hh\:mm");
                ddlHoraFin.SelectedValue = horario.HorarioFin.ToString(@"hh\:mm");
                lblAgregarHorario.Text = "Modificar Horario";

                collapseOne.Attributes["class"] = "accordion-collapse collapse show";
            }
            else
            {
                alert.Visible = false;
                btnCancelar.Visible = false;
                gvHorarioMedico.Visible = true;
                collapseOne.Attributes["class"] = "accordion-collapse collapse";
            }
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            Medico medico = (Medico)Session["medico"];


            int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

            TurnoTrabajoNegocio turnoNegocio = new TurnoTrabajoNegocio();
            HorarioAtencionNegocio horarioNegocio = new HorarioAtencionNegocio();
            TimeSpan horarioInicio = TimeSpan.Parse(ddlHoraInicio.SelectedItem.Value);
            TimeSpan horarioFin = TimeSpan.Parse(ddlHoraFin.SelectedItem.Value);
            TurnoTrabajo turno = turnoNegocio.existeTurno(horarioInicio, horarioFin);
            if (turno == null)
            {
                turnoNegocio.InsertarTurnoTrabajo("Turno de " + horarioFin + " a " + horarioFin, horarioInicio, horarioFin);
                turno = turnoNegocio.existeTurno(horarioInicio, horarioFin);
            }
            if (Session["idModificarHorario"] == null)
            {
                if (horarioNegocio.existeHorario(horarioInicio, horarioFin, medico, ObtenerNumeroDiaSemana(ddlDia.SelectedItem.Value)))
                {
                    alert.Visible = true;
                    return;
                }
                horarioNegocio.InsertarHorarioAtencion(medico.IdMedico, idEspecialidad, turno.IdTurnoTrabajo, ObtenerNumeroDiaSemana(ddlDia.SelectedItem.Value));
                HorarioAtencionNegocio horarioAtencionNegocio = new HorarioAtencionNegocio();
                cargarGridHorario();
                Response.Redirect("HorarioMedico.aspx", false);
            }
            else
            {
                int id = (int)Session["idModificarHorario"];
                if (horarioNegocio.existeHorarioDistinto(horarioInicio, horarioFin, medico, ObtenerNumeroDiaSemana(ddlDia.SelectedItem.Value), id))
                {
                    alert.Visible = true;
                    return;
                }
                HorarioAtencion horario = new HorarioAtencion
                {
                    Id = id,
                    DiaSemana = ObtenerNumeroDiaSemana(ddlDia.SelectedItem.Value),
                    Especialidad = new Especialidad { Id = idEspecialidad },
                    HorarioFin = horarioFin,
                    HorarioInicio = horarioInicio,
                    Medico = new Medico { IdMedico = medico.IdMedico },
                    Turno = turno
                };
                HorarioAtencionNegocio negocio = new HorarioAtencionNegocio();
                negocio.modificarHorario(horario);
                Session.Remove("idModificarHorario");
                Response.Redirect("HorarioMedico.aspx", false);
            }
            alert.Visible = false;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            alert.Visible = false;
            Session.Remove("idModificarHorario");
            Response.Redirect("HorarioMedico.aspx");
        }

        public string ObtenerNombreDia(object dia)
        {
            int numero = Convert.ToInt32(dia);

            switch (numero)
            {
                case 1: return "Lunes";
                case 2: return "Martes";
                case 3: return "Miércoles";
                case 4: return "Jueves";
                case 5: return "Viernes";
                case 6: return "Sábado";
                case 7: return "Domingo";
                default: return "Desconocido";
            }
        }
        private int ObtenerNumeroDiaSemana(string dia)
        {
            dia = System.Web.HttpUtility.HtmlDecode(dia).ToLower();

            switch (dia)
            {
                case "lunes": return 1;
                case "martes": return 2;
                case "miércoles":
                case "miercoles": return 3;
                case "jueves": return 4;
                case "viernes": return 5;
                case "sábado":
                case "sabado": return 6;
                case "domingo": return 7;
                default: throw new ArgumentException("Día inválido: " + dia);
            }
        }

        protected void gvHorarioMedico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvHorarioMedico.DataKeys[index].Value);
            HorarioAtencionNegocio negocio = new HorarioAtencionNegocio();
            if (e.CommandName == "Eliminar")
            {
                negocio.eliminarLogico(id);
                cargarGridHorario();
            }

            if (e.CommandName == "Editar")
            {
                Session.Add("IdModificarHorario", id);
                Response.Redirect("HorarioMedico.aspx", true);
            }

        }

        protected void ddlHoraInicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            string hora = ddlHoraInicio.SelectedItem.Value;
            int horaEntero = int.Parse(hora.Split(':')[0]);
            cargarDropDowns(horaEntero);
        }
    }
}