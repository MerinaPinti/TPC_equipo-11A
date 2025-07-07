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
            int idMedico = Convert.ToInt32(Session["idMedico"]);
            if (idMedico == null)
            {
                Response.Redirect("Default.aspx");
                return;
            }
            Session["paginaAnterior"] = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

            if (!IsPostBack)
            {
                cargarDropDowns();
                cargarGridHorario();
                controlarFormularioModificacion();

                //    HorarioAtencionNegocio horarioAtencionNegocio = new HorarioAtencionNegocio();
                //    gvHorarioMedico.DataSource = horarioAtencionNegocio.listar();
                //    gvHorarioMedico.DataBind();

                //    List<String> dias = new List<String> { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo" };
                //    ddlDia.DataSource = dias;
                //    ddlDia.DataBind();

                //    List<String> horarios = new List<String>();
                //    for (int i = 0; i < 24; i++)
                //    {
                //        horarios.Add(i.ToString("D2") + ":00");
                //    }
                //    ddlHoraFin.DataSource = horarios;
                //    ddlHoraInicio.DataSource = horarios;
                //    ddlHoraFin.DataBind();
                //    MedicoNegocio negocio = new MedicoNegocio();
                //    ddlHoraInicio.DataBind();
                //    Usuario usuario = (Usuario)Session["usuario"];
                //    Medico medico = negocio.existeMedico(usuario);
                //    ddlEspecialidad.DataSource = medico.Especialidad;
                //    ddlEspecialidad.DataTextField = "Descripcion";
                //    ddlEspecialidad.DataValueField = "Id";
                //    ddlEspecialidad.DataBind();
                //}

                //btnCancelar.Visible = false;
                //gvHorarioMedico.Visible = true;
                //collapseOne.Attributes["class"] = "accordion-collapse collapse";

                //if (Session["idModificarHorario"] != null)
                //{
                //    int id = (int)Session["idModificarHorario"];
                //    HorarioAtencionNegocio horarioNegocio = new HorarioAtencionNegocio();
                //    HorarioAtencion horario = horarioNegocio.listar(id);
                //    btnCancelar.Visible = true;
                //    gvHorarioMedico.Visible = false;
                //    ddlDia.SelectedIndex = horario.DiaSemana - 1;
                //    ddlEspecialidad.SelectedValue = horario.Especialidad.Id.ToString();
                //    ddlHoraInicio.SelectedValue = horario.HorarioInicio.ToString(@"hh\:mm");
                //    ddlHoraFin.SelectedValue = horario.HorarioFin.ToString(@"hh\:mm");
                //    lblAgregarHorario.Text = "Modificar Horario";
                //    collapseOne.Attributes["class"] = "accordion-collapse collapse show";
                //}
                //else
                //{
                //    btnCancelar.Visible = false;
                //    gvHorarioMedico.Visible = true;
                //}


                //cargarEspecialidades(idMedico);
                //CargarDias();
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

                ddlDia.SelectedIndex = horario.DiaSemana - 1;
                ddlEspecialidad.SelectedValue = horario.Especialidad.Id.ToString();
                ddlHoraInicio.SelectedValue = horario.HorarioInicio.ToString(@"hh\:mm");
                ddlHoraFin.SelectedValue = horario.HorarioFin.ToString(@"hh\:mm");
                lblAgregarHorario.Text = "Modificar Horario";

                collapseOne.Attributes["class"] = "accordion-collapse collapse show";
            }
            else
            {
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
                horarioNegocio.InsertarHorarioAtencion(medico.IdMedico, idEspecialidad, turno.IdTurnoTrabajo, ObtenerNumeroDiaSemana(ddlDia.SelectedItem.Value));
                HorarioAtencionNegocio horarioAtencionNegocio = new HorarioAtencionNegocio();
                cargarGridHorario();
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
            else
            {
                int id = (int)Session["idModificarHorario"];
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
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
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

        //protected void gvHorarioMedico_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        DropDownList ddlHoraInicio = (DropDownList)e.Row.FindControl("ddlHoraInicio");
        //        DropDownList ddlHoraFin = (DropDownList)e.Row.FindControl("ddlHoraFin");

        //        if (ddlHoraInicio != null && ddlHoraFin != null)
        //        {
        //            for (int h = 0; h < 24; h++)
        //            {
        //                string hora = $"{h:D2}:00";
        //                ddlHoraInicio.Items.Add(hora);
        //                ddlHoraFin.Items.Add(hora);
        //            }

        //            // Valor por defecto 
        //            ddlHoraInicio.SelectedValue = "00";
        //            ddlHoraFin.SelectedValue = "00";
        //        }
        //    }
        //}

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
                Session.Add("IdModificarHorario", id);
                Response.Redirect("HorarioMedico.aspx", true);
            }

        }
    }
}