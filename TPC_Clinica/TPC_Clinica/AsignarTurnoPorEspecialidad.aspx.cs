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
                if (!IsPostBack)
                {
                    EspecialidadNegocio negocio = new EspecialidadNegocio();
                    ddlEspecialidades.DataSource = negocio.Listar();
                    ddlEspecialidades.DataTextField = "Descripcion";
                    ddlEspecialidades.DataValueField = "Id";
                    ddlEspecialidades.DataBind();

                    ddlEspecialidades.Items.Insert(0, new ListItem("-- Seleccione una especialidad --", ""));
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
            //el Today toma el día actual de sistema y le agrega hasta 30 días al calendario (para que no se muestren todos los turnos juntos, sino que 30 días nomás)
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
                                bool yaAsignado = turnosAsignados.Any(t =>
                                    t.Fecha.Date == fecha.Date &&
                                    (int)t.Hora.TotalHours == hora &&
                                    t.Medico.IdMedico == idMedico &&
                                    t.Estado != null &&
                                    t.Estado.Id == 1);

                                string estadoTexto = yaAsignado ? "Asignado" : "Disponible";

                                eventos.Add(new
                                {
                                    title = $"{hora.ToString("D2")}:00 - {estadoTexto}",
                                    start = new DateTime(fecha.Year, fecha.Month, fecha.Day, hora, 0, 0).ToString("s"),
                                    allDay = false,
                                    backgroundColor = yaAsignado ? "green" : "lightblue",
                                    borderColor = yaAsignado ? "darkgreen" : "blue",
                                    textColor = "white",
                                    extendedProps = new
                                    {
                                        hora = hora.ToString("D2") + ":00",
                                        estado = estadoTexto,
                                        idTurno = turnosAsignados.FirstOrDefault(t =>
                                            t.Fecha.Date == fecha.Date &&
                                            (int)t.Hora.TotalHours == hora &&
                                            t.Medico.IdMedico == idMedico &&
                                            t.Estado.Id == 1)?.NroTurno ?? "0",
                                        nombrePaciente = turnosAsignados.FirstOrDefault(t =>
                                            t.Fecha.Date == fecha.Date &&
                                            (int)t.Hora.TotalHours == hora &&
                                            t.Medico.IdMedico == idMedico &&
                                            t.Estado.Id == 1)?.Paciente?.NombreCompleto ?? "", 
                                        nombreMedico = turnosAsignados.FirstOrDefault(t =>
                                            t.Fecha.Date == fecha.Date &&
                                            (int)t.Hora.TotalHours == hora &&
                                            t.Medico.IdMedico == idMedico &&
                                            t.Estado.Id == 1)?.Medico?.NombreCompleto ?? ""
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


            //Chekea que el paciente se encuentra registrado 
            if (paciente == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "noExiste", "alert('El paciente no está registrado.');", true);
                return;
            }

            // Obtenemos los datos del turno de los campos ocultos 
            string fechaStr = hfFechaTurno.Value;
            string horaStr = hfHoraTurno.Value;
            string idMedicoStr = hfIdMedico.Value;

            //Valida que hayamos podido conseguir toda la info que necesitamos (sino, falla alguno de los campos ocultos)
            if (string.IsNullOrEmpty(fechaStr) || string.IsNullOrEmpty(horaStr) || string.IsNullOrEmpty(idMedicoStr))

            {
                ScriptManager.RegisterStartupScript(this, GetType(), "faltanDatos", "alert('Faltan datos del turno.');", true);
                return;
            }

            //Generamos un nuevo turno con la info que corresponde. 
            Turno nuevo = new Turno
            {
                Paciente = paciente,
                Medico = new Medico { IdMedico = int.Parse(idMedicoStr) },
                Fecha = DateTime.Parse(fechaStr),
                Hora = TimeSpan.Parse(horaStr),
                Estado = new Estado { Id = 1 } // Asignado (antes tenía null)
            };

            TurnoNegocio negocio = new TurnoNegocio();
            negocio.agregarTurno(nuevo);

            ScriptManager.RegisterStartupScript(this, GetType(), "turnoAsignado",
                $"alert('Turno asignado correctamente a {paciente.Nombre} {paciente.Apellido}.');", true);

            txtDniPaciente.Text = "";
            hfFechaTurno.Value = "";
            hfHoraTurno.Value = "";

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
        }


        [System.Web.Services.WebMethod]
        public static bool CancelarTurno(int idTurno)
        {
            try
            {
                TurnoNegocio negocio = new TurnoNegocio();

                Turno turno = new Turno
                {
                    NroTurno = idTurno.ToString(),
                    Estado = new Estado { Id = 3 } // Cancelado
                };

                negocio.actualizarTurnoRecep(turno);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }


    }