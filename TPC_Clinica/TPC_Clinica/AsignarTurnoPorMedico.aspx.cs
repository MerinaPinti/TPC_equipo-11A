using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Clinica
{
    public partial class AsignarTurnoPorMedico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Usuario usuario = (Usuario)Session["usuario"] != null ? (Usuario)Session["usuario"] : null;
            if (usuario == null || usuario.TipoUsuario.Id == 3)
            {
                Session["error"] = "No tiene permisos para acceder a esta página.";
                Response.Redirect("Error.aspx", true);
            }
            Session["paginaAnterior"] = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

            if (!IsPostBack)
            {
                
            }
        }

        // Botón para redirigir al calendario en otra página 
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

        // Boton que dispara la carga de especialidades (está oculto)
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

        // Evento al seleccionar una especialidad que luego carga el  calendario
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

       

        // No lo estoy usando aún. 
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
                //Guarda el DNI en sesion
                Session["DniParaRegistrar"] = dniIngresado;

                // Redirige al alta de paciente
                Response.Redirect("AltaPaciente.aspx?from=asignacionTurno");
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
                Especialidad = new Especialidad { Id = Convert.ToInt32(ddlEspecialidades.SelectedValue), Descripcion = ddlEspecialidades.SelectedItem.Text },
                Hora = TimeSpan.Parse(horaStr),
                Estado = new Estado { Id = 1 } // Asignado
            };

            // Inserta el turno
            TurnoNegocio negocio = new TurnoNegocio();
            negocio.agregarTurno(nuevo);

            // Confirmación 
            ScriptManager.RegisterStartupScript(this, GetType(), "turnoAsignado",
                $"alert('Turno asignado correctamente a {paciente.Nombre} {paciente.Apellido}.');", true);

            //------------------------------ENVIO DE MAIL------------------------------
            string rutaPlantillas = Server.MapPath("~/Templates");

            MedicoNegocio medicoNegocio = new MedicoNegocio();
            nuevo.Medico = medicoNegocio.existeMedico(nuevo.Medico.IdMedico);
            var reemplazos = new Dictionary<string, string>
            {
                { "NOMBRE", nuevo.Paciente.Nombre + " " + nuevo.Paciente.Apellido },
                { "MEDICO", nuevo.Medico.Nombre + " " + nuevo.Medico.Apellido },
                { "FECHA", nuevo.Fecha.ToString("dd/MM/yyyy")},
                { "HORA", nuevo.Hora.ToString(@"hh\:mm") }
            };

            EmailService emailService = new EmailService();
            emailService.armarCorreo(
                paciente.Email,
                "Turno asignado en Clínica Médica Meraki 💙",
                reemplazos,
                TipoCorreo.EmailConfirmarTurno,
                rutaPlantillas
            );
            emailService.enviarCorreo();
            //-------------------------------------------------------------------------
            
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
                    // Verifica si la fecha coincide con el día de la semana configurado en el horario(h.DiaSemana).
                    //% 7 porque en .NET el DayOfWeek va de 0(domingo) a 6(sábado), y en tu base DiaSemana va de 1(lunes) a 7(domingo) sirve para poder comparar e igualar. 
                    if ((int)fecha.DayOfWeek == (h.DiaSemana % 7))
                    {
                        for (int hora = h.Turno.HoraInicio.Hours; hora < h.Turno.HoraFin.Hours; hora++)
                        {
                            //Verifica si ya existe un turno asignado para ese médico, esa fecha y esa hora.
                            // Usa t.Hora.TotalHours para comparar solo la hora(sin minutos ni segundos).
                            // Estado.Id == 1 implica que es un turno "Asignado".
                            bool yaAsignado = turnosAsignados.Any(t =>
                                t.Fecha.Date == fecha.Date &&
                                (int)t.Hora.TotalHours == hora &&
                                t.Medico.IdMedico == idMedico &&
                                t.Estado != null &&
                                t.Estado.Id == 1);

                            string estadoTexto = yaAsignado ? "Asignado" : "Disponible";

                            // Si está asignado, obtenemos el turno correspondiente para incluir info de paciente y médico
                            Turno turno = null;
                            if (yaAsignado)
                            {
                                turno = turnosAsignados.FirstOrDefault(t =>
                                    t.Fecha.Date == fecha.Date &&
                                    (int)t.Hora.TotalHours == hora &&
                                    t.Medico.IdMedico == idMedico);
                            }

                            //Se crea un objeto anónimo para representar el evento del calendario.
                            //title: muestra la hora y estado.
                            // start: construye la fecha y hora completa en formato ISO(ToString("s")). allDay = false: indica que el evento tiene hora específica. Esto para poder independizar cada horario que aparece en el calendario
                            eventos.Add(new
                            {
                                title = $"{hora:00}:00 - {estadoTexto}",
                                start = new DateTime(fecha.Year, fecha.Month, fecha.Day, hora, 0, 0).ToString("s"),
                                allDay = false,
                                backgroundColor = yaAsignado ? "green" : "lightblue",
                                borderColor = yaAsignado ? "darkgreen" : "blue",
                                textColor = "white",
                                extendedProps = new
                                {
                                    hora = $"{hora:00}:00",
                                    estado = estadoTexto,
                                    idTurno = turno != null ? turno.NroTurno : "",
                                    nombrePaciente = turno?.Paciente != null ? turno.Paciente.Nombre + " " + turno.Paciente.Apellido : "",
                                    nombreMedico = turno?.Medico != null ? turno.Medico.Nombre + " " + turno.Medico.Apellido : ""
                                }
                            });
                        }
                    }
                }
            }

            return eventos;
        }


        [WebMethod]
        public static bool CancelarTurno(int idTurno)
        {
            try
            {
                // Obtener el turno completo con paciente y médico
                TurnoNegocio negocio = new TurnoNegocio();
                Turno turno = negocio.ObtenerPorId(idTurno.ToString());

                // Cambiar el estado
                turno.Estado = new Estado { Id = 3 }; // Cancelado
                negocio.actualizarTurnoRecep(turno);

                //------------------------------ENVIO DE MAIL------------------------------
                string rutaPlantillas = HttpContext.Current.Server.MapPath("~/Templates");

                var reemplazos = new Dictionary<string, string>
                {
                    { "NOMBRE", turno.Paciente.Nombre + " " + turno.Paciente.Apellido },
                    { "FECHA", turno.Fecha.ToString("dd/MM/yyyy") + " a las " + turno.Hora.ToString(@"hh\:mm") },
                    { "MEDICO", turno.Medico.Nombre + " " + turno.Medico.Apellido },
                };

                EmailService emailService = new EmailService();
                emailService.armarCorreo(
                    turno.Paciente.Email,
                    "Cancelación de turno en Clínica Médica Meraki 💙",
                    reemplazos,
                    TipoCorreo.EmailCancelarTurno,
                    rutaPlantillas
                );

                if (EmailService.EsEmailValido(turno.Paciente.Email))
                {
                    emailService.enviarCorreo();

                }
                else
                {
                    Console.WriteLine("Email inválido, no se enviará el correo.");
                }

                //-------------------------------------------------------------------------
                return true;
            }
            catch
            {
                return false;
            }
        }



    }


}

    
    
