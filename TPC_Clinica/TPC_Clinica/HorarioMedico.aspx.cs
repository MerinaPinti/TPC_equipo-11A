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
            if (!IsPostBack)
            {
                int idMedico = Convert.ToInt32(Session["idMedico"]);
                if (Session["idMedico"] == null)
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
                Session["paginaAnterior"] = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

                HorarioAtencionNegocio horarioAtencionNegocio = new HorarioAtencionNegocio();
                gvHorarioMedico.DataSource = horarioAtencionNegocio.listar();
                gvHorarioMedico.DataBind();

                List<String> dias = new List<String> { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo" };
                List<String> horarios = new List<String>();
                MedicoNegocio negocio = new MedicoNegocio();
                Usuario usuario = (Usuario)Session["usuario"];
                Medico medico = negocio.existeMedico(usuario);

                for (int i = 0; i < 24; i++)
                {
                    horarios.Add(i.ToString() + ":00");
                }
                ddlDia.DataSource = dias;
                ddlEspecialidad.DataSource = medico.Especialidad;
                ddlEspecialidad.DataTextField = "Descripcion";
                ddlEspecialidad.DataValueField = "Id";
                ddlHoraFin.DataSource = horarios;
                ddlHoraInicio.DataSource = horarios;
                ddlDia.DataBind();
                ddlEspecialidad.DataBind();
                ddlHoraFin.DataBind();
                ddlHoraInicio.DataBind();


                //cargarEspecialidades(idMedico);
                //CargarDias();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //Id del login
            int idMedico = Convert.ToInt32(Session["idMedico"]);
            //id del desplegable
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

            horarioNegocio.InsertarHorarioAtencion(idMedico, idEspecialidad, turno.IdTurnoTrabajo, ObtenerNumeroDiaSemana(ddlDia.SelectedItem.Value));
            HorarioAtencionNegocio horarioAtencionNegocio = new HorarioAtencionNegocio();
            gvHorarioMedico.DataSource = horarioAtencionNegocio.listar();
            gvHorarioMedico.DataBind();
            //verifica dato por dato de la grilla de horario
            //foreach (GridViewRow fila in gvHorarioMedico.Rows)
            //{
            //    string diaTexto = fila.Cells[0].Text.Trim();
            //    int diaSemana = ObtenerNumeroDiaSemana(diaTexto);

            //    //Si está clickeado el check box sigue al siguiente 
            //    CheckBox chkDiaLibre = (CheckBox)fila.FindControl("chkDiaLibre");
            //    if (chkDiaLibre != null && chkDiaLibre.Checked)
            //        continue;

            //    DropDownList ddlInicio = (DropDownList)fila.FindControl("ddlHoraInicio");
            //    DropDownList ddlFin = (DropDownList)fila.FindControl("ddlHoraFin");

            //    string horaInicio = ddlInicio.SelectedValue;
            //    string horaFin = ddlFin.SelectedValue;

            //    if (!string.IsNullOrEmpty(horaInicio) && !string.IsNullOrEmpty(horaFin))
            //    {
            //        // Verifica si existe el turno de trabajo seleccionado (Es decir los horarios). 
            //        int idTurnoTrabajo = turnoNegocio.ObtenerIdTurnoTrabajo(horaInicio, horaFin);

            //        // En caso de que no exista lo crea desde cero (rango horario). 
            //        if (idTurnoTrabajo == 0)
            //        {
            //            idTurnoTrabajo = turnoNegocio.InsertarTurnoTrabajo(
            //                "Turno " + horaInicio + " - " + horaFin,
            //                TimeSpan.Parse(horaInicio),
            //                TimeSpan.Parse(horaFin)
            //            );
            //        }

            //        // Hace un insert a la tabla HORARIO ATENCIÓN con los datos recolectados hasta acá 
            //        horarioNegocio.InsertarHorarioAtencion(idMedico, idEspecialidad, idTurnoTrabajo, diaSemana);
            //    }
            //}

            // Msj Exitoso. 
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            Response.Redirect("Default.aspx");
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

        protected void gvHorarioMedico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlHoraInicio = (DropDownList)e.Row.FindControl("ddlHoraInicio");
                DropDownList ddlHoraFin = (DropDownList)e.Row.FindControl("ddlHoraFin");

                if (ddlHoraInicio != null && ddlHoraFin != null)
                {
                    for (int h = 0; h < 24; h++)
                    {
                        string hora = $"{h:D2}:00";
                        ddlHoraInicio.Items.Add(hora);
                        ddlHoraFin.Items.Add(hora);
                    }

                    // Valor por defecto 
                    ddlHoraInicio.SelectedValue = "00";
                    ddlHoraFin.SelectedValue = "00";
                }
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
                gvHorarioMedico.DataSource = negocio.listar();
                gvHorarioMedico.DataBind();

            }

            if (e.CommandName == "Editar")
            {
                Session.Add("IdModificarEspecialidad", id);
                Response.Redirect("AltaEspecialidad.aspx", true);
            }

        }
    }
}