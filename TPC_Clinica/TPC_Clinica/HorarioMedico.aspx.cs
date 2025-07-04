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

                cargarEspecialidades(idMedico);
                CargarDias();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //Id del login
            int idMedico = Convert.ToInt32(Session["idMedico"]);
            //id del desplegable
            int idEspecialidad = int.Parse(ddlEspecialidades.SelectedValue);

            TurnoTrabajoNegocio turnoNegocio = new TurnoTrabajoNegocio();
            HorarioAtencionNegocio horarioNegocio = new HorarioAtencionNegocio();

            //verifica dato por dato de la grilla de horario
            foreach (GridViewRow fila in gvHorarioMedico.Rows)
            {
                string diaTexto = fila.Cells[0].Text.Trim();
                int diaSemana = ObtenerNumeroDiaSemana(diaTexto);

                //Si está clickeado el check box sigue al siguiente 
                CheckBox chkDiaLibre = (CheckBox)fila.FindControl("chkDiaLibre");
                if (chkDiaLibre != null && chkDiaLibre.Checked)
                    continue;

                DropDownList ddlInicio = (DropDownList)fila.FindControl("ddlHoraInicio");
                DropDownList ddlFin = (DropDownList)fila.FindControl("ddlHoraFin");

                string horaInicio = ddlInicio.SelectedValue;
                string horaFin = ddlFin.SelectedValue;

                if (!string.IsNullOrEmpty(horaInicio) && !string.IsNullOrEmpty(horaFin))
                {
                    // Verifica si existe el turno de trabajo seleccionado (Es decir los horarios). 
                    int idTurnoTrabajo = turnoNegocio.ObtenerIdTurnoTrabajo(horaInicio, horaFin);

                    // En caso de que no exista lo crea desde cero (rango horario). 
                    if (idTurnoTrabajo == 0)
                    {
                        idTurnoTrabajo = turnoNegocio.InsertarTurnoTrabajo(
                            "Turno " + horaInicio + " - " + horaFin,
                            TimeSpan.Parse(horaInicio),
                            TimeSpan.Parse(horaFin)
                        );
                    }

                    // Hace un insert a la tabla HORARIO ATENCIÓN con los datos recolectados hasta acá 
                    horarioNegocio.InsertarHorarioAtencion(idMedico, idEspecialidad, idTurnoTrabajo, diaSemana);
                }
            }

            // Msj Exitoso. 
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Horario guardado correctamente'); window.location='HorarioMedico.aspx';", true);
        }



        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            Response.Redirect("Default.aspx");
        }

        //Genera una lista de 7 objetos DiaHorario, uno por cada día de la semana para completar el dgv 
        private void CargarDias()
        {
            List<DiaHorario> dias = new List<DiaHorario>
    {
        new DiaHorario { Dia = "Lunes" },
        new DiaHorario { Dia = "Martes" },
        new DiaHorario { Dia = "Miércoles" },
        new DiaHorario { Dia = "Jueves" },
        new DiaHorario { Dia = "Viernes" },
        new DiaHorario { Dia = "Sábado" },
        new DiaHorario { Dia = "Domingo" }
    };

            gvHorarioMedico.DataSource = dias;
            gvHorarioMedico.DataBind();
        }
        //CLASE LOCAL PARA GENERAR EL LISTADO DE DÍAS en el GRID 
        public class DiaHorario
        {
            public string Dia { get; set; }
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

        private void cargarEspecialidades(int idMedico)
        {
            EspecialidadNegocio negocio = new EspecialidadNegocio();
            List<Especialidad> lista = negocio.ListarPorMedico(idMedico);

            ddlEspecialidades.DataSource = lista;
            ddlEspecialidades.DataTextField = "Descripcion";
            ddlEspecialidades.DataValueField = "Id";
            ddlEspecialidades.DataBind();

            ddlEspecialidades.Items.Insert(0, new ListItem("-- Seleccione una especialidad --", ""));
        }
    }
}