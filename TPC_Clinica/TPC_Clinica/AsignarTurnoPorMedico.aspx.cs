using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace TPC_Clinica
{
    public partial class AsignarTurnoPorMedico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Al inicio no cargamos nada porque usamos el buscador tipo autocomplete.
            }
        }

        // Botón para redirigir al calendario en otra página (por si se usa)
        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hfIdMedico.Value) || string.IsNullOrEmpty(ddlEspecialidades.SelectedValue))
                return;

            int idMedico = int.Parse(hfIdMedico.Value);
            int idEspecialidad = int.Parse(ddlEspecialidades.SelectedValue);

            Session["idMedicoSeleccionado"] = idMedico;
            Session["idEspecialidadSeleccionada"] = idEspecialidad;

            Response.Redirect("CalendarioTurnos.aspx");
        }

        // WebMethod para autocomplete de médico
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static List<MedicoAutocomplete> BuscarMedico(string prefix)
        {
            MedicoNegocio negocio = new MedicoNegocio();
            List<Medico> medicos = negocio.listar();

            var coincidencias = medicos
                .Where(m => m.NombreCompleto.ToLower().Contains(prefix.ToLower()))
                .Select(m => new MedicoAutocomplete
                {
                    label = m.NombreCompleto,
                    value = m.IdMedico.ToString()
                }).ToList();

            return coincidencias;
        }

        [System.Serializable]
        public class MedicoAutocomplete
        {
            public string label { get; set; }
            public string value { get; set; }
        }

        // Botón oculto que se dispara desde JS para cargar especialidades
        protected void btnCargarEspecialidades_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfIdMedico.Value))
            {
                int idMedico = int.Parse(hfIdMedico.Value);
                EspecialidadNegocio negocio = new EspecialidadNegocio();
                ddlEspecialidades.DataSource = negocio.ListarPorMedico(idMedico);
                ddlEspecialidades.DataTextField = "Descripcion";
                ddlEspecialidades.DataValueField = "Id";
                ddlEspecialidades.DataBind();
                ddlEspecialidades.Items.Insert(0, new ListItem("-- Seleccione una especialidad --", ""));
            }
        }

        // Evento al seleccionar una especialidad -> carga calendario
        protected void ddlEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfIdMedico.Value) && !string.IsNullOrEmpty(ddlEspecialidades.SelectedValue))
            {
                int idMedico = int.Parse(hfIdMedico.Value);
                int idEspecialidad = int.Parse(ddlEspecialidades.SelectedValue);

                // Hacemos visible el panel del calendario
                pnlCalendario.Visible = true;

                // Ejecutamos función JS que carga el calendario
                ScriptManager.RegisterStartupScript(this, this.GetType(), "cargarCalendario",
                    $"cargarCalendario({idMedico}, {idEspecialidad});", true);
            }
        }

        // WebMethod que será llamado por JavaScript (FullCalendar) para traer los turnos disponibles
        [System.Web.Services.WebMethod]
        public static List<object> ObtenerTurnosDisponibles(int idMedico, int idEspecialidad)
        {
            HorarioAtencionNegocio horarioNegocio = new HorarioAtencionNegocio();
            List<HorarioAtencion> horarios = horarioNegocio.ListarPorMedicoYEspecialidad(idMedico, idEspecialidad);

            var eventos = new List<object>();

            DateTime hoy = DateTime.Today;
            DateTime fin = hoy.AddDays(30); // Mostramos próximos 30 días

            foreach (var h in horarios)
            {
                for (DateTime fecha = hoy; fecha <= fin; fecha = fecha.AddDays(1))
                {
                    // DíaSemana en la base: 1=Lunes ... 7=Domingo
                    // En .NET DayOfWeek: 0=Domingo ... 6=Sábado
                    if ((int)fecha.DayOfWeek == (h.DiaSemana % 7))
                    {
                        eventos.Add(new
                        {
                            title = $"Disponible {h.Turno.HoraInicio:hh\\:mm}-{h.Turno.HoraFin:hh\\:mm}",
                            start = fecha.ToString("yyyy-MM-dd"),
                            allDay = true
                        });
                    }
                }
            }

            return eventos;
        }

        // (No se usa directamente ahora, pero útil si volvemos a mostrar una tabla en vez de calendario)
        private string ObtenerNombreDia(int numeroDia)
        {
            switch (numeroDia)
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

        protected void btnAsignarTurno_Click(object sender, EventArgs e)
        {
            string dni = txtDniPaciente.Text;

            if (string.IsNullOrWhiteSpace(dni))
            {
                // Podés mostrar un mensaje de error con JavaScript o literal si querés
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Por favor, ingrese un DNI válido.');", true);
                return;
            }

            // Acá deberías buscar si existe el paciente con ese DNI
            // Y si existe, registrar el turno en la base de datos (lo implementamos después)

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ok", "alert('Turno asignado exitosamente (falta implementar lógica real).');", true);

            // Cierre del modal opcional (desde JS)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "cerrarModal", "$('#modalAsignarTurno').modal('hide');", true);
        }

    }


}

    
    
