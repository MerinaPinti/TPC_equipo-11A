using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Clinica
{
    public partial class AsignarTurno2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Session["error"] = "Debe iniciar sesión para acceder a esta página.";
                Response.Redirect("Error.aspx", true);
            }
            Session["paginaAnterior"] = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
        }

        protected void btnEspecialidad_Click(object sender, EventArgs e)
        {
            Response.Redirect("AsignarTurnoPorEspecialidad.aspx");
        }

        protected void btnProfesional_Click(object sender, EventArgs e)
        {
            Response.Redirect("AsignarTurnoPorMedico.aspx");
        }
    }
}