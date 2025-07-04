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
                CargarDias();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

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

                    // Valor por defecto (opcional)
                    ddlHoraInicio.SelectedValue = "08:00";
                    ddlHoraFin.SelectedValue = "17:00";
                }
            }
        }
    }
}