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
    public partial class Master : System.Web.UI.MasterPage
    {
        public Usuario usuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                Usuario usuario = (Usuario)Session["usuario"];
                int rol = usuario.TipoUsuario.Id;

                switch (rol)
                {
                    case 1:
                        hlEspecialidades.Visible = true;
                        hlEstados.Visible = true;
                        hlMedicos.Visible = true;
                        hlPacientes.Visible = true;
                        hlTipoUsuario.Visible = true;
                        hlTurnos.Visible = true;
                        hlUsuarios.Visible = true;
                        hpMeraki.NavigateUrl = "~/Inicio.aspx";
                        hlCambiarClave.Visible = true;
                        hlHistoriaClinica.Visible = true;
                        btnCerrarSesion.Visible = true;
                        hlModificarTurno.Visible = true;
                        hlTurnos.Visible = true;
                        break;
                    case 2:
                        hlEspecialidades.Visible = true;
                        hlMedicos.Visible = true;
                        hlPacientes.Visible = true;
                        hlTurnos.Visible = true;
                        hpMeraki.NavigateUrl = "~/Inicio.aspx";
                        hlCambiarClave.Visible = true;
                        hlHistoriaClinica.Visible = true;
                        btnCerrarSesion.Visible = true;
                        hlModificarTurno.Visible = true;
                        hlTurnos.Visible = true;
                        break;
                    case 3:
                        hlTurnos.Visible = true;
                        hlPacientes.Visible = true;
                        hlTurnos.Text = "Mis turnos";
                        hlPacientes.Text = "Mis pacientes";
                        hlTurnos.NavigateUrl = "VerTurnoMedico.aspx";
                        hpMeraki.NavigateUrl = "~/Inicio.aspx";
                        hlHorarios.Visible = true;
                        hlCambiarClave.Visible = true;
                        hlHistoriaClinica.Visible = true;
                        btnCerrarSesion.Visible = true;
                        hlModificarTurno.Visible = false;
                        break;
                    default:
                        hlEspecialidades.Visible = false;
                        hlEstados.Visible = false;
                        hlMedicos.Visible = false;
                        hlPacientes.Visible = false;
                        hlTipoUsuario.Visible = false;
                        hlTurnos.Visible = false;
                        hlUsuarios.Visible = false;
                        hlCambiarClave.Visible = false;
                        hlHistoriaClinica.Visible = false;
                        btnCerrarSesion.Visible = false;
                        hlModificarTurno.Visible = false;
                        hlTurnos.Visible = false;
                        break;
                }

            }

        }
        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Default.aspx"); 
        }
    }
}
