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
    public partial class AsignarTurnoPorEspecialidad : System.Web.UI.Page
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
                EspecialidadNegocio negocio = new EspecialidadNegocio();
                ddlEspecialidades.DataSource = negocio.Listar();
                ddlEspecialidades.DataTextField = "Descripcion";
                ddlEspecialidades.DataValueField = "Id";
                ddlEspecialidades.DataBind();

                ddlEspecialidades.Items.Insert(0, new ListItem("-- Seleccione una especialidad --", ""));
            }
            if (Session["DNI_Reasignacion"] != null)
            {
                txtDniPaciente.Text = Session["DNI_Reasignacion"].ToString();
                txtDniPaciente.Enabled = false;
            }
        }

        protected void ddlEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddlEspecialidades.SelectedValue, out int idEspecialidad))
            {
                MedicoNegocio negocio = new MedicoNegocio();
                List<Medico> medicos = negocio.ListarPorEspecialidad(idEspecialidad);

                ddlMedicos.DataSource = medicos;
                ddlMedicos.DataTextField = "NombreCompleto";
                ddlMedicos.DataValueField = "IdMedico";
                ddlMedicos.DataBind();

                ddlMedicos.Items.Insert(0, new ListItem("-- Seleccione un médico --", ""));
            }
            else
            {
                ddlMedicos.Items.Clear();
                ddlMedicos.Items.Insert(0, new ListItem("-- Seleccione un médico --", ""));
            }
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
                            var turnoCoincidente = turnosAsignados.FirstOrDefault(t =>
                                t.Fecha.Date == fecha.Date &&
                                (int)t.Hora.TotalHours == hora &&
                                t.Medico.IdMedico == idMedico &&
                                t.Estado != null);

                            string estadoTexto = "Disponible";
                            string backgroundColor = "lightblue";
                            string borderColor = "blue";

                            if (turnoCoincidente != null)
                            {
                                switch (turnoCoincidente.Estado.Id)
                                {
                                    case 1: // Asignado
                                        estadoTexto = "Asignado";
                                        backgroundColor = "green";
                                        borderColor = "darkgreen";
                                        break;
                                    case 4: // No asistió
                                        estadoTexto = "No asistió";
                                        backgroundColor = "#ff6666"; // rojo claro
                                        borderColor = "#cc0000";
                                        break;
                                    case 5: // Atendido
                                        estadoTexto = "Atendido";
                                        backgroundColor = "#999999"; // gris
                                        borderColor = "#666666";
                                        break;
                                    default:
                                        estadoTexto = "Otro";
                                        backgroundColor = "#cccccc";
                                        borderColor = "#999999";
                                        break;
                                }
                            }

                            eventos.Add(new
                            {
                                title = $"{hora.ToString("D2")}:00 - {estadoTexto}",
                                start = new DateTime(fecha.Year, fecha.Month, fecha.Day, hora, 0, 0).ToString("s"),
                                allDay = false,
                                backgroundColor = backgroundColor,
                                borderColor = borderColor,
                                textColor = "white",
                                extendedProps = new
                                {
                                    hora = hora.ToString("D2") + ":00",
                                    estado = estadoTexto,
                                    idTurno = turnoCoincidente?.NroTurno ?? "0",
                                    nombrePaciente = turnoCoincidente?.Paciente?.NombreCompleto ?? "",
                                    nombreMedico = turnoCoincidente?.Medico?.NombreCompleto ?? ""
                                }
                            });
                        }
                    }
                }
            }

            return eventos;
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
                Session["DniParaRegistrar"] = dniIngresado;
                Response.Redirect("AltaPaciente.aspx?from=asignacionTurno");
                return;
            }

            string fechaStr = hfFechaTurno.Value;
            string horaStr = hfHoraTurno.Value;
            string idMedicoStr = hfIdMedico.Value;
            string idTurnoExistente = hfIdTurnoACancelar.Value; //Sirve para ver si un turno fue cancelado

            if (string.IsNullOrEmpty(fechaStr) || string.IsNullOrEmpty(horaStr) || string.IsNullOrEmpty(idMedicoStr))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "faltanDatos", "alert('Faltan datos del turno.');", true);
                return;
            }

            TurnoNegocio negocio = new TurnoNegocio();
            MedicoNegocio medicoNegocio = new MedicoNegocio();

            if (!string.IsNullOrEmpty(idTurnoExistente) && idTurnoExistente != "0")
            {
                // Es un turno existente (cancelado previamente) entonces se actualiza 
                Turno turnoExistente = negocio.ObtenerPorId(idTurnoExistente);
                turnoExistente.Paciente = paciente;
                turnoExistente.Estado = new Estado { Id = 1 }; // Asignado
                negocio.actualizarTurnoRecep(turnoExistente);

                // Refrescamos calendario
                ScriptManager.RegisterStartupScript(this, GetType(), "turnoAsignado", "alert('Turno reasignado correctamente.'); if(window.calendar) { window.calendar.refetchEvents(); }", true);
            }
            else
            {
                // Turno nuevo en caso de que no existiera previamente. 
                Turno nuevo = new Turno
                {
                    Paciente = paciente,
                    Medico = new Medico { IdMedico = int.Parse(idMedicoStr) },
                    Fecha = DateTime.Parse(fechaStr),
                    Especialidad = new Especialidad { Id = Convert.ToInt32(ddlEspecialidades.SelectedValue), Descripcion = ddlEspecialidades.SelectedItem.Text },
                    Hora = TimeSpan.Parse(horaStr),
                    Estado = new Estado { Id = 1 }
                };

                negocio.agregarTurno(nuevo);

                string script = $@"
            alert('Turno asignado correctamente a {paciente.Nombre} {paciente.Apellido}.');
            if (window.calendar) {{
                window.calendar.refetchEvents();
            }}";
                ScriptManager.RegisterStartupScript(this, GetType(), "turnoAsignado", script, true);
            }

            // ----------------- MAIL -----------------
            string rutaPlantillas = Server.MapPath("~/Templates");
            Medico medico = medicoNegocio.existeMedico(int.Parse(idMedicoStr));
            var reemplazos = new Dictionary<string, string>
    {
        { "NOMBRE", paciente.Nombre + " " + paciente.Apellido },
        { "MEDICO", medico.Nombre + " " + medico.Apellido },
        { "FECHA", DateTime.Parse(fechaStr).ToString("dd/MM/yyyy")},
        { "HORA", TimeSpan.Parse(horaStr).ToString(@"hh\:mm") }
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
            // ---------------------------------------

            txtDniPaciente.Text = "";
            hfFechaTurno.Value = "";
            hfHoraTurno.Value = "";
            hfIdTurnoACancelar.Value = ""; 
        }


        [System.Web.Services.WebMethod]
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

        //OBTENEMOS TODOS LOS TURNOS Y LE ASIGNAMOS DISTINTOS COLORES SEGÚN EL ESTADO EN EL CALENDARIO.

        [System.Web.Services.WebMethod]
        public static List<object> ObtenerTodosLosTurnos(int idMedico, int idEspecialidad)
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
                            var turnoCoincidente = turnosAsignados.FirstOrDefault(t =>
                                t.Fecha.Date == fecha.Date &&
                                (int)t.Hora.TotalHours == hora &&
                                t.Medico.IdMedico == idMedico &&
                                t.Estado != null);

                            string estadoTexto = "Disponible";
                            string backgroundColor = "lightblue";
                            string borderColor = "blue";

                            if (turnoCoincidente != null)
                            {
                                switch (turnoCoincidente.Estado.Id)
                                {
                                    case 1:
                                        estadoTexto = "Asignado";
                                        backgroundColor = "green";
                                        borderColor = "darkgreen";
                                        break;
                                    case 3:
                                        estadoTexto = "Cancelado";
                                        backgroundColor = "#ffc107"; // amarillo
                                        borderColor = "#e0a800";
                                        break;
                                    case 4:
                                        estadoTexto = "No asistió";
                                        backgroundColor = "#dc3545"; // rojo
                                        borderColor = "#a71d2a";
                                        break;
                                    case 5:
                                        estadoTexto = "Atendido";
                                        backgroundColor = "#6c757d"; // gris
                                        borderColor = "#343a40";
                                        break;
                                }
                            }

                            eventos.Add(new
                            {
                                title = $"{hora.ToString("D2")}:00 - {estadoTexto}",
                                start = new DateTime(fecha.Year, fecha.Month, fecha.Day, hora, 0, 0).ToString("s"),
                                allDay = false,
                                backgroundColor,
                                borderColor,
                                textColor = "white",
                                extendedProps = new
                                {
                                    hora = hora.ToString("D2") + ":00",
                                    estado = estadoTexto,
                                    idTurno = turnoCoincidente?.NroTurno ?? "0",
                                    nombrePaciente = turnoCoincidente?.Paciente?.NombreCompleto ?? "",
                                    nombreMedico = turnoCoincidente?.Medico?.NombreCompleto ?? ""
                                }
                            });
                        }
                    }
                }
            }

            return eventos;
        }


    }


}
