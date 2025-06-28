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

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string user = txtusuario.Text;
            string pass = txtpassword.Text;

            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario logueado = negocio.Login(user, pass);

            if (logueado != null)
            {
                Session["usuario"] = logueado;
                Response.Redirect("EleccionTurno.aspx");
            }
            else
            {
                Response.Redirect("Error.aspx?msg=Usuario o contraseña incorrectos");
            }
        }
    }
}