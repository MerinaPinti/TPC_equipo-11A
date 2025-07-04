using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

                
                cargarEspecialidades(idMedico);
                CargarDias();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //Id del login 
            int idMedico = Convert.ToInt32(Session["idMedico"]);  
             

            foreach (GridViewRow fila in gvHorarioMedico.Rows)
            {
                string diaTexto = fila.Cells[0].Text.Trim();
                int diaSemana = ObtenerNumeroDiaSemana(diaTexto); // Lunes = 1, Domingo = 7 etc.

                CheckBox chkDiaLibre = (CheckBox)fila.FindControl("chkDiaLibre");
                if (chkDiaLibre != null && chkDiaLibre.Checked)
                    continue; // Si tiene el check sigue con el otro

                DropDownList ddlInicio = (DropDownList)fila.FindControl("ddlHoraInicio");
                DropDownList ddlFin = (DropDownList)fila.FindControl("ddlHoraFin");

                string horaInicio = ddlInicio.SelectedValue;
                string horaFin = ddlFin.SelectedValue;

            }
        }

        

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            Response.Redirect("Default.aspx");
        }

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
                    ddlHoraInicio.SelectedValue = "00:00";
                    ddlHoraFin.SelectedValue = "00:00";
                }
            }
        }

        private int ObtenerNumeroDiaSemana(string dia)
        {
            switch (dia.ToLower())
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