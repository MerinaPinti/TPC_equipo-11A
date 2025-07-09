using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Clinica
{
    public partial class CambiarClaveDeUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Session["error"] = "Debe iniciar sesión para acceder a esta página.";
                Response.Redirect("Error.aspx", true);
            }
            Session["paginaAnterior"] = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

            if (!IsPostBack)
            {
                lblActual.Text = "";
                lblNueva.Text = "";

                lblActual.Visible = false;
                lblNueva.Visible = false;
            }
        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            string actual = txtActual.Text.Trim();
            string nueva = txtNueva.Text.Trim();
            string confirmar = txtConfirmar.Text.Trim();

            Usuario usuarioActual = Session["usuario"] as Usuario;
            //No inició sesión
            if (usuarioActual == null)
            {
                Response.Redirect("Login.aspx");                
            }

            if (!validar())
            {
                return;
            }
            
            try
            {
                usuarioActual.Password = nueva;
                UsuarioNegocio negocio = new UsuarioNegocio();            
                negocio.modificarUsuario(usuarioActual);

                lblActual.Text = "¡Contraseña actualizada correctamente!";
                lblActual.ForeColor = System.Drawing.Color.Green;
                lblActual.Visible = true;
            }
            catch (Exception ex)
            {
                Session["error"] = ex.ToString();
                Response.Redirect("Error.aspx", false);
            }
        }

        protected bool validar()
        {
            Usuario usuarioActual = Session["usuario"] as Usuario;
            string actual = txtActual.Text.Trim();
            string nueva = txtNueva.Text.Trim();
            string confirmar = txtConfirmar.Text.Trim();
            bool validar = true;

            //Campos vacíos
            if (string.IsNullOrEmpty(actual) || string.IsNullOrEmpty(nueva) || string.IsNullOrEmpty(confirmar))
            {
                lblActual.Text = "Completa todos los campos.";
                lblActual.ForeColor = System.Drawing.Color.Red;
                lblActual.Visible = true;
                validar = false;
            }
            //Contraseña actual erronea
            else if (actual != usuarioActual.Password)
            {
                lblActual.Text = "La contraseña actual es incorrecta.";
                lblActual.ForeColor = System.Drawing.Color.Red;
                lblActual.Visible = true;
                validar = false;
            }
            //Contraseñas diferentes
            else if (nueva != confirmar)
            {
                lblNueva.Text = "La nueva contraseña no coincide con su confirmación.";
                lblNueva.ForeColor = System.Drawing.Color.Red;
                lblNueva.Visible = true;
                validar = false;
            }

            return validar;
        }
    }
}
