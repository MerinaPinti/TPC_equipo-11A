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
    public partial class EleccionTurno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AsignarTurno.aspx");

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ModificarTurno.aspx");

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("CancelarTurno.aspx");
        }
    }
}