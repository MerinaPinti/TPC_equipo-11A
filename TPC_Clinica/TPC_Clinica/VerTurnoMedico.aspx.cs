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
            Usuario usuario = (Usuario)Session["usuario"] != null ? (Usuario)Session["usuario"] : null;
            if (usuario == null)
            {
                Session["error"] = "No tiene permisos para acceder a esta página.";
                Response.Redirect("Error.aspx", true);
            }

            Session["paginaAnterior"] = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

            if (!IsPostBack)
            {
                // Obtener el ID del médico desde sesión
                idMedico = ((Medico)Session["medico"]).IdMedico;

                // Cargar directamente los turnos del médico en sala de espera
                CargarTurnosEnSalaEspera(idMedico);
            }
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

                Usuario medicoLogueado = (Usuario)Session["usuario"];
                if (medicoLogueado != null)
                {
                    CargarTurnosEnSalaEspera(medicoLogueado.Id);

                    lblMensaje.Text = $"El turno {nroTurno} fue marcado como <strong>'No asistió'</strong>.";
                    lblMensaje.CssClass = "alert alert-info";
                    lblMensaje.Visible = true;
                }
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

        private void CargarTurnosEnSalaEspera(int idMedico)
        {
            TurnoNegocio negocio = new TurnoNegocio();
            List<Turno> lista = negocio.ListarTurnosDelDiaMedico(idMedico);

            var tabla = lista.Select(t => new TurnoVista
            {
                NroTurno = t.NroTurno,
                Fecha = t.Fecha.ToString("dd/MM/yyyy"),
                Hora = t.Hora.ToString(@"hh\:mm"),
                NombrePaciente = $"{t.Paciente.Nombre} {t.Paciente.Apellido}",
                Especialidad = t.Especialidad.Descripcion,

                // Comentado hasta que se agregue el campo en la entidad Turno y en BD
                // MotivoConsulta = t.MotivoConsulta,

                IdEstado = t.Estado.Id
            }).ToList();

            gvTurnos.DataSource = tabla;
            gvTurnos.DataBind();

            if (tabla.Count == 0)
            {
                lblMensaje.Text = "No hay pacientes en sala de espera en este momento.";
                lblMensaje.Visible = true;
            }
            else
            {
                lblMensaje.Visible = false;
            }
        }
    }
}
