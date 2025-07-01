using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Clinica
{
    public partial class SeleccionarEspecialidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((string)Session["usuario"]))
            {
                Session["error"] = "Debe iniciar sesión para acceder a esta página.";
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {

        }

    }
}