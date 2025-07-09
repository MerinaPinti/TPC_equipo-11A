using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace TPC_Clinica
{
    public partial class ModificarTurno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Session["error"] = "Debe iniciar sesión con permisos para acceder a esta página.";
                Response.Redirect("Error.aspx", true);
            }
            Session["paginaAnterior"] = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
        }

        protected void dgvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();

            List<Turno> turnos = Session["TurnosPorDNI"] as List<Turno>;
            Turno turno = turnos?.FirstOrDefault(t => t.NroTurno == id);

            if (turno == null)
            {
                return;
            }

            if (e.CommandName == "Modificar")
            {
                Turno turnoCancelado = new Turno
                {
                    NroTurno = id.ToString(),
                    Estado = new Estado { Id = 3 } // Cancelado
                };

                TurnoNegocio negocio = new TurnoNegocio();
                negocio.actualizarTurnoRecep(turnoCancelado);
                //------------------------------ENVIO DE MAIL------------------------------
                string rutaPlantillas = HttpContext.Current.Server.MapPath("~/Templates");

                var reemplazos = new Dictionary<string, string>
                {
                    { "NOMBRE", turno.Paciente.Nombre + " " + turno.Paciente.Apellido },
                    { "FECHA", turno.Fecha.ToString("dd/MM/yyyy") + " a las " + turno.Hora.ToString(@"hh\:mm")},
                    { "MEDICO", turno.Medico.Nombre + " " + turno.Medico.Apellido },
                };

                EmailService emailService = new EmailService();
                emailService.armarCorreo(
                    turno.Paciente.Email,
                    "Cancelacion de Turno en Clínica Médica Meraki 💙",
                    reemplazos,
                    TipoCorreo.EmailCancelarTurno,
                    rutaPlantillas
                );
                emailService.enviarCorreo();
                //-------------------------------------------------------------------------

                Session["DNI_Reasignacion"] = turno.Paciente?.DNI;

                Response.Redirect("AsignarTurno2.aspx", true);
            }
            else if (e.CommandName == "Cancelar")
            {
                Turno turnocancelado = new Turno
                {
                    NroTurno = id.ToString(),
                    Estado = new Estado { Id = 3 } // Cancelado
                };
                TurnoNegocio negocio = new TurnoNegocio();
                negocio.actualizarTurnoRecep(turnocancelado);
                //------------------------------ENVIO DE MAIL------------------------------
                string rutaPlantillas = HttpContext.Current.Server.MapPath("~/Templates");

                var reemplazos = new Dictionary<string, string>
                {
                    { "NOMBRE", turno.Paciente.Nombre + " " + turno.Paciente.Apellido },
                    { "FECHA", turno.Fecha.ToString("dd/MM/yyyy") + " a las " + turno.Hora.ToString(@"hh\:mm")},
                    { "MEDICO", turno.Medico.Nombre + " " + turno.Medico.Apellido },
                };

                EmailService emailService = new EmailService();
                emailService.armarCorreo(
                    turno.Paciente.Email,
                    "Cancelacion de Turno en Clínica Médica Meraki 💙",
                    reemplazos,
                    TipoCorreo.EmailCancelarTurno,
                    rutaPlantillas
                );
                emailService.enviarCorreo();
                //-------------------------------------------------------------------------
                btnBuscar_Click(null, null); // Recarga grilla
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string dni = txtBuscarDNI.Text.Trim();
            if (string.IsNullOrEmpty(dni))
                return;

            TurnoNegocio negocio = new TurnoNegocio();
            List<Turno> lista = negocio.ListarTurnosPorDNI(dni,1);

            if (lista != null && lista.Count > 0)
            {
                Session["TurnosPorDNI"] = lista;

                var visuales = lista.Select(t => new
                {
                    NroTurno = t.NroTurno,
                    Fecha = t.Fecha.ToString("dd/MM/yyyy"),
                    Hora = t.Hora.ToString(@"hh\:mm"),
                    Medico = t.Medico.Nombre,
                    Especialidad = t.Medico.Especialidad?.FirstOrDefault()?.Descripcion ?? "Sin especialidad",
                    Estado = t.Estado.Descripcion
                }).ToList();

                dgvTurnos.DataSource = visuales;
                dgvTurnos.DataBind();
            }
            else
            {
                dgvTurnos.DataSource = null;
                dgvTurnos.DataBind();

            }
        }

    }

}
