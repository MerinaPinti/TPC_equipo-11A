using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Clinica
{
    public partial class ListadoMedicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MedicoNegocio negocio = new MedicoNegocio();
                GridView1.DataSource = negocio.listar(); 
                GridView1.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaMedico.aspx");
        }
    }
}