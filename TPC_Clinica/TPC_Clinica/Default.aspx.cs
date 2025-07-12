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
    public partial class FormuPrincipal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["paginaAnterior"] = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string user = txtusuario.Text.Trim();
            string pass = txtpassword.Text.Trim();

            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario logueado = negocio.Login(user, pass);

            if (logueado != null)
            {
                Session["usuario"] = logueado;

                // Verificamos si el tipo de usuario es Médico (ID 3)
                if (logueado.TipoUsuario != null && logueado.TipoUsuario.Id == 3)
                {
                    MedicoNegocio medicoNegocio = new MedicoNegocio();
                    Medico medicoLogueado = medicoNegocio.ObtenerPorIdUsuario(logueado.Id);

                    if (medicoLogueado != null)
                    {
                        Session["medico"] = medicoLogueado;
                        Response.Write("ID Médico logueado: " + medicoLogueado.IdMedico);
                    }
                }
                if (logueado.TipoUsuario != null && logueado.TipoUsuario.Id == 2)
                {
                    Response.Redirect("Recepcion.aspx");
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            else
            {
                Session["paginaAnterior"] = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                Session.Add("error", "Usuario o contraseña incorrectos");
                Response.Redirect("Error.aspx", false);
            }
        }



    }
}
