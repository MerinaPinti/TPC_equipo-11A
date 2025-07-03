using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Clinica
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty((string)Session["error"]))
                {
                    lblMensaje.Text = Session["error"].ToString();
                    Session.Remove("error");
                }
                else lblMensaje.Text = "Erorr";
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            string urlVolver = Session["paginaAnterior"].ToString();
            Response.Redirect(urlVolver); 
        }
    }
}