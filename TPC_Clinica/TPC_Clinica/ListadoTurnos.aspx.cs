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
    public partial class ListadoTurnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFiltros();
                // AplicarFiltros(); // 
            }
        }
       
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            AplicarFiltros(); 
        }

        private void CargarFiltros()
        {
            // Especialidades
            var especialidades = new EspecialidadNegocio().Listar();
            ddlEspecialidad.DataSource = especialidades;
            ddlEspecialidad.DataTextField = "Descripcion";
            ddlEspecialidad.DataValueField = "Id";
            ddlEspecialidad.DataBind();
            ddlEspecialidad.Items.Insert(0, new ListItem("-- Todas --", ""));

            // Médicos
            var medicos = new MedicoNegocio().listar(); 
            ddlMedico.DataSource = medicos;
            ddlMedico.DataTextField = "NombreCompleto"; 
            ddlMedico.DataValueField = "IdMedico";
            ddlMedico.DataBind();
            ddlMedico.Items.Insert(0, new ListItem("-- Todos --", ""));

            // Estados
            var estados = new EstadoNegocio().Listar(); 
            ddlEstado.DataSource = estados;
            ddlEstado.DataTextField = "Descripcion";
            ddlEstado.DataValueField = "Id";
            ddlEstado.DataBind();
            ddlEstado.Items.Insert(0, new ListItem("-- Todos --", ""));
        }


        private void AplicarFiltros()
        {
            int idEspecialidad = int.TryParse(ddlEspecialidad.SelectedValue, out int esp) ? esp : 0;
            int idMedico = int.TryParse(ddlMedico.SelectedValue, out int med) ? med : 0;
            int idEstado = int.TryParse(ddlEstado.SelectedValue, out int est) ? est : 0;
            string filtroPaciente = txtPaciente.Text.Trim().ToLower();

            TurnoNegocio negocio = new TurnoNegocio();
            List<Turno> lista = negocio.ListarTurnos(); // Trae todos los turnos activos

            //  FILTRO por especialidad
            if (idEspecialidad != 0)
                lista = lista.Where(t => t.Especialidad != null && t.Especialidad.Id == idEspecialidad).ToList();

            //  FILTRO por médico
            if (idMedico != 0)
                lista = lista.Where(t => t.Medico.IdMedico == idMedico).ToList();

            //  FILTRO por estado
            if (idEstado != 0)
                lista = lista.Where(t => t.Estado.Id == idEstado).ToList();

            // FILTRO por paciente (DNI o nombre completo)
            if (!string.IsNullOrEmpty(filtroPaciente))
            {
                lista = lista.Where(t =>
                    (t.Paciente.DNI?.ToLower().Contains(filtroPaciente) ?? false) ||
                    ($"{t.Paciente.Nombre} {t.Paciente.Apellido}".ToLower().Contains(filtroPaciente))
                ).ToList();
            }

            // VALIDACIÓN: Médico no tiene esa especialidad
            if (idEspecialidad != 0 && idMedico != 0)
            {
                MedicoNegocio medicoNegocio = new MedicoNegocio();
                var especialidadesDelMedico = medicoNegocio.ListarEspecialidadesPorMedico(idMedico);
                bool tieneEspecialidad = especialidadesDelMedico.Any(e => e.Id == idEspecialidad);

                if (!tieneEspecialidad)
                {
                    gvTurnos.DataSource = null;
                    gvTurnos.DataBind();
                    MostrarMensaje("El médico seleccionado no posee la especialidad elegida.");
                    return;
                }
            }

            // VALIDACIÓN: No se encontraron turnos para el paciente ingresado
            if (!string.IsNullOrEmpty(filtroPaciente) && lista.Count == 0)
            {
                gvTurnos.DataSource = null;
                gvTurnos.DataBind();
                MostrarMensaje("No se encontraron turnos para el paciente ingresado.");
                return;
            }

            // Resultado 
            if (lista.Count == 0)
            {
                gvTurnos.DataSource = null;
                gvTurnos.DataBind();
                MostrarMensaje("No hay resultados para la búsqueda.");
                return;
            }

            //  Arma la vista para el GridView
            var tabla = lista.Select(t => new TurnoVista
            {
              
    Fecha = t.Fecha.ToString("dd/MM/yyyy"),
                Hora = t.Hora.ToString(@"hh\:mm"),
                NombrePaciente = t.Paciente != null ? $"{t.Paciente.Nombre} {t.Paciente.Apellido}" : "—",
    DniPaciente = t.Paciente?.DNI ?? "—",
    NombreMedico = t.Medico != null ? $"{t.Medico.Nombre} {t.Medico.Apellido}" : "—",
    Especialidad = t.Especialidad?.Descripcion ?? "—",
    Estado = t.Estado?.Descripcion ?? "—"
}).ToList();

            gvTurnos.DataSource = tabla;
            gvTurnos.DataBind();
        }

        private void MostrarMensaje(string texto)
        {
            lblMensaje.Text = texto;
            lblMensaje.CssClass = "alert alert-warning";
            lblMensaje.Visible = true;
        }


    }
}
