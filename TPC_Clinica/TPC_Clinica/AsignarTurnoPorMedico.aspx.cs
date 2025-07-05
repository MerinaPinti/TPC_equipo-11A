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
                    // No cargamos nada acá porque usamos el buscador autocomplete.
                }
            }

            // Botón que guarda el médico y especialidad seleccionados en sesión y redirige
            protected void btnContinuar_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrEmpty(hfIdMedico.Value) || string.IsNullOrEmpty(ddlEspecialidades.SelectedValue))
                {
                    // Validación simple por si no se seleccionó alguno
                    return;
                }

                int idMedico = int.Parse(hfIdMedico.Value);
                int idEspecialidad = int.Parse(ddlEspecialidades.SelectedValue);

                // Guardamos el ID del médico seleccionado y la Especialidad Seleccionada. 
                Session["idMedicoSeleccionado"] = idMedico;
                Session["idEspecialidadSeleccionada"] = idEspecialidad;

                Response.Redirect("CalendarioTurnos.aspx");
            }

            //AUTOCOMPLETE A LA HORA DE BUSCAR EL MÉDICO
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

            //Clase usada por el autocomplete (etiqueta y valor que se muestran)
            [System.Serializable]
            public class MedicoAutocomplete
            {
                public string label { get; set; } // Nombre completo
                public string value { get; set; } // ID del médico
            }

            //Carga las especialidades en el DropdownList (según médico seleccionado)
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

            //Cuando seleccionamos una especialidad, mostramos los turnos disponibles en esa especialidad para ese médico
            protected void ddlEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (!string.IsNullOrEmpty(hfIdMedico.Value) && !string.IsNullOrEmpty(ddlEspecialidades.SelectedValue))
                {
                    int idMedico = int.Parse(hfIdMedico.Value);
                    int idEspecialidad = int.Parse(ddlEspecialidades.SelectedValue);
                    MostrarTurnosDisponibles(idMedico, idEspecialidad);
                }
            }

            // En este método listamos los turnos disponibles según médico y especialidad
            private void MostrarTurnosDisponibles(int idMedico, int idEspecialidad)
            {
                HorarioAtencionNegocio horarioNegocio = new HorarioAtencionNegocio();
                List<HorarioAtencion> horarios = horarioNegocio.ListarPorMedicoYEspecialidad(idMedico, idEspecialidad);

                if (horarios.Count == 0)
                {
                    // En caso de no tener turnos cargados mostramos el mensaje
                    litTurnosDisponibles.Text = "<p class='text-danger'>Este médico no tiene horarios disponibles para esta especialidad.</p>";
                    pnlCalendario.Visible = true;
                    return;
                }

                // Generamos una tabla básica (más adelante lo cambiamos por un calendario interactivo si querés)
                string html = "<table class='table table-bordered'><thead><tr><th>Día</th><th>Horario</th></tr></thead><tbody>";

                foreach (var h in horarios)
                {
                    string dia = ObtenerNombreDia(h.DiaSemana);
                    string rango = h.Turno.HoraInicio.ToString(@"hh\:mm") + " - " + h.Turno.HoraFin.ToString(@"hh\:mm");

                    html += $"<tr><td>{dia}</td><td>{rango}</td></tr>";
                }

                html += "</tbody></table>";

                litTurnosDisponibles.Text = html;
                pnlCalendario.Visible = true;
            }

            // Convierte número de día en nombre (1 = Lunes, 2 = Martes, etc.)
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
        }

    }
    
