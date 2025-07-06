using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
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
            string dniIngresado = txtDniPaciente.Text.Trim();

            if (string.IsNullOrEmpty(dniIngresado))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "dniVacio", "alert('Por favor, ingrese el DNI del paciente.');", true);
                return;
            }

            PacienteNegocio pacienteNegocio = new PacienteNegocio();
            Paciente paciente = pacienteNegocio.existePaciente(dniIngresado);

            if (paciente == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "noExiste", "alert('El paciente no está registrado. Por favor, registre al paciente antes de asignar un turno.');", true);
                return;
            }

            // Obtenemos los datos del turno desde los HiddenField
            string fechaStr = hfFechaTurno.Value;
            string horaStr = hfHoraTurno.Value;

            if (string.IsNullOrEmpty(fechaStr) || string.IsNullOrEmpty(horaStr) || string.IsNullOrEmpty(hfIdMedico.Value))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "faltanDatos", "alert('Faltan datos del turno.');", true);
                return;
            }

            // Se crea el objeto turno con la información del calendario más el paciente ingresado en la modal
            Turno nuevo = new Turno
            {
                Paciente = paciente,
                Medico = new Medico { IdMedico = int.Parse(hfIdMedico.Value) },
                Fecha = DateTime.Parse(fechaStr),
                
                Hora = TimeSpan.Parse(horaStr),
                Estado = new Estado { Id = 1 } // Asignado
            };

            // Inserta el turno
            TurnoNegocio negocio = new TurnoNegocio();
            negocio.agregarTurno(nuevo);

            // Confirmación 
            ScriptManager.RegisterStartupScript(this, GetType(), "turnoAsignado",
                $"alert('Turno asignado correctamente a {paciente.Nombre} {paciente.Apellido}.');", true);

            // Se limpian los datos
            txtDniPaciente.Text = "";
            hfFechaTurno.Value = "";
            hfHoraTurno.Value = "";
        }


        [System.Web.Services.WebMethod]
        public static List<object> ObtenerTurnosDisponibles(int idMedico, int idEspecialidad)
        {
            HorarioAtencionNegocio horarioNegocio = new HorarioAtencionNegocio();
            TurnoNegocio turnoNegocio = new TurnoNegocio();

            List<HorarioAtencion> horarios = horarioNegocio.ListarPorMedicoYEspecialidad(idMedico, idEspecialidad);
            List<Turno> turnosAsignados = turnoNegocio.ListarTurnosAsignados(idMedico);

            var eventos = new List<object>();
            DateTime hoy = DateTime.Today;
            DateTime fin = hoy.AddDays(30);

           

            foreach (var h in horarios)
            {
                for (DateTime fecha = hoy; fecha <= fin; fecha = fecha.AddDays(1))
                {
                    if ((int)fecha.DayOfWeek == (h.DiaSemana % 7))
                    {
                        for (int hora = h.Turno.HoraInicio.Hours; hora < h.Turno.HoraFin.Hours; hora++)
                        {
                            TimeSpan horaTurno = new TimeSpan(hora, 0, 0); // 

                            bool yaAsignado = turnosAsignados.Any(t =>
                                t.Fecha.Date == fecha.Date &&
                                t.Hora == horaTurno &&
                                t.Medico.IdMedico == idMedico &&
                                t.Estado != null &&
                                t.Estado.Id == 1
                            );

                            string estadoTexto = yaAsignado ? "Asignado" : "Disponible";
                            string color = yaAsignado ? "green" : "blue";

                            eventos.Add(new
                            {
                                title = $"{hora:D2}:00 - {estadoTexto}",
                                start = $"{fecha:yyyy-MM-dd}T{hora:D2}:00:00",
                                allDay = false,
                                color = color
                            });
                        }
                    }
                }
            }

            return eventos;
        }



    }


}

    
    
