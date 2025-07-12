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
    public partial class Recepcion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEspecialidades();
                CargarTurnos();
            }
        }


        protected void txtDni_TextChanged(object sender, EventArgs e)
        {

            CargarTurnos();
        }

        protected void ddlEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTurnos();
        }

        protected void txtMedico_TextChanged(object sender, EventArgs e)
        {
            CargarTurnos();
        }

        private void CargarTurnos()
        {
            string dni = txtDni.Text.Trim();
            string nombreMedico = txtMedico.Text.Trim();
            int idEspecialidad = int.TryParse(ddlEspecialidades.SelectedValue, out int val) ? val : 0;

            TurnoNegocio negocio = new TurnoNegocio();

            List<Turno> lista = negocio.ListarTurnosDelDia(dni, nombreMedico, idEspecialidad);

            // adaptamos los datos para el grid
            var tabla = lista.Select(t => new
            {
                NroTurno = t.NroTurno,
                Hora = t.Hora.ToString(@"hh\:mm"),
                PacienteNombre = $"{t.Paciente.Nombre} {t.Paciente.Apellido}",
                DniPaciente = t.Paciente.DNI,
                Especialidad = t.Especialidad.Descripcion,
                MedicoNombre = $"{t.Medico.Nombre} {t.Medico.Apellido}",
                EstadoTurno = t.Estado.Descripcion,
                IdEstado = t.Estado.Id
            }).ToList();

            gvTurnos.DataSource = tabla;
            gvTurnos.DataBind();
            //Verifica si hay filtros y cuenta los registros de la lista, es decir si trajo.. 
            bool hayFiltros = !string.IsNullOrEmpty(txtDni.Text.Trim()) ||
                  !string.IsNullOrEmpty(txtMedico.Text.Trim()) ||
                  (ddlEspecialidades.SelectedIndex > 0);

            if (lista.Count == 0 && hayFiltros)
            {
                //Si no hay filtros aplicados y no hay datos en la lista se muestra el msj 
                lblMensaje.Text = "No se encontraron turnos para los filtros aplicados.";
                lblMensaje.Visible = true;
            }
            else
            {
                lblMensaje.Visible = false;
            }

        }

        private void CargarEspecialidades()
        {
            EspecialidadNegocio negocio = new EspecialidadNegocio();
            List<Especialidad> lista = negocio.Listar();

            ddlEspecialidades.DataSource = lista;
            ddlEspecialidades.DataTextField = "Descripcion";
            ddlEspecialidades.DataValueField = "Id";
            ddlEspecialidades.DataBind();

            ddlEspecialidades.Items.Insert(0, new ListItem("Todas", "0")); // default
        }

        protected void gvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Asistio")
            {
                int idTurno = Convert.ToInt32(e.CommandArgument);

                Turno turno = new Turno
                {
                    NroTurno = idTurno.ToString(),
                    Estado = new Estado { Id = 6 } // En sala de espera
                };

                TurnoNegocio negocio = new TurnoNegocio();
                negocio.actualizarTurnoMedico(turno);

                CargarTurnos(); // refresca el grid
            }
            else if (e.CommandName == "NoAsistio")
            {
                int nroTurno = Convert.ToInt32(e.CommandArgument);

                Turno turno = new Turno
                {
                    NroTurno = nroTurno.ToString(),
                    Estado = new Estado { Id = 4 } // No Asistió
                };

                TurnoNegocio negocio = new TurnoNegocio();
                negocio.actualizarTurnoMedico(turno);

                CargarTurnos();
            }
        }
    }
}
