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
    public partial class AtenderPaciente : System.Web.UI.Page
    {
    
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    Turno turno = Session["turnoAAtender"] as Turno;
                    if (turno == null)
                    {
                        Response.Redirect("VerTurnoMedico.aspx");
                        return;
                    }

                    // Mostrar datos
                    lblPaciente.Text = turno.Paciente.Nombre + " " + turno.Paciente.Apellido;
                    lblFecha.Text = turno.Fecha.ToShortDateString();
                    lblHora.Text = turno.Hora.ToString(@"hh\:mm");
                    txtObservaciones.Text = turno.Observaciones;
                    txtDiagnostico.Text = turno.Diagnostico;

                    // Guardar en ViewState el NroTurno
                    ViewState["nroTurno"] = turno.NroTurno;
                }
            }

            protected void btnGuardar_Click(object sender, EventArgs e)
            {
                string nroTurno = ViewState["nroTurno"]?.ToString();
                if (string.IsNullOrEmpty(nroTurno))
                {
                    Response.Redirect("VerTurnoMedico.aspx");
                    return;
                }

                Turno turno = new Turno
                {
                    NroTurno = nroTurno,
                    Estado = new Estado { Id = 5 }, // 5 = Cerrado
                    Observaciones = txtObservaciones.Text,
                    Diagnostico = txtDiagnostico.Text
                };

                TurnoNegocio negocio = new TurnoNegocio();
                negocio.actualizarTurnoMedico(turno);

                Response.Redirect("VerTurnoMedico.aspx");
            }
        }

    }
