using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Negocio.TurnoNegocio;

namespace TPC_Clinica
{
    public partial class VerTurnoMedico : System.Web.UI.Page
    {
        private int idMedico;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            idMedico = ((Medico)Session["medico"]).IdMedico;

            if (!IsPostBack)
            {
                EspecialidadNegocio negocio = new EspecialidadNegocio();
                ddlEspecialidades.DataSource = negocio.ListarPorMedico(idMedico);
                ddlEspecialidades.DataTextField = "Descripcion";
                ddlEspecialidades.DataValueField = "Id";
                ddlEspecialidades.DataBind();

                ddlEspecialidades.Items.Insert(0, new ListItem("-- Seleccione Especialidad --", ""));
            }
        }

        protected void ddlEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlEspecialidades.SelectedValue))
            {
                gvTurnos.DataSource = null;
                gvTurnos.DataBind();
                return;
            }

            int idEspecialidad = int.Parse(ddlEspecialidades.SelectedValue);
            int idMedico = ((Medico)Session["medico"]).IdMedico;

            TurnoNegocio negocio = new TurnoNegocio();
            gvTurnos.DataSource = negocio.ListarTurnosAsignadosSemana(idMedico, idEspecialidad);
            gvTurnos.DataBind();
        }


        protected void gvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvTurnos.Rows[index];
            string nroTurno = gvTurnos.DataKeys[index].Value.ToString();

            if (e.CommandName == "Atender")
            {
                TurnoNegocio negocio = new TurnoNegocio();
                Turno turno = negocio.ObtenerPorId(nroTurno);

                Session["turnoAAtender"] = turno;
                Response.Redirect("AtenderPaciente.aspx");
            }
            else if (e.CommandName == "NoAsistio")
            {
                Turno turno = new Turno
                {
                    NroTurno = nroTurno,
                    Estado = new Estado { Id = 4 } // No Asistió
                };

                TurnoNegocio negocio = new TurnoNegocio();
                negocio.actualizarTurnoMedico(turno);

                ddlEspecialidades_SelectedIndexChanged(null, null); // refrescar grilla
            }
        }

        protected void gvTurnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TurnoVista turno = (TurnoVista)e.Row.DataItem;

                if (turno.IdEstado == 5)
                {
                    e.Row.Cells[3].Controls.Clear();
                    e.Row.Cells[4].Controls.Clear();

                    TableCell cell = new TableCell
                    {
                        ColumnSpan = 2,
                        Text = "<span class='badge bg-success'>Atendido</span>",
                        HorizontalAlign = HorizontalAlign.Center
                    };

                    e.Row.Cells.Add(cell);
                }
                else if (turno.IdEstado == 4)
                {
                    e.Row.Cells[3].Controls.Clear();
                    e.Row.Cells[4].Controls.Clear();

                    TableCell cell = new TableCell
                    {
                        ColumnSpan = 2,
                        Text = "<span class='badge bg-danger'>No asistió</span>",
                        HorizontalAlign = HorizontalAlign.Center
                    };

                    e.Row.Cells.Add(cell);
                }
            }
        }
    }
}
