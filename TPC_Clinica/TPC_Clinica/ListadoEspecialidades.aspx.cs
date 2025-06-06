using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_Clinica
{
    public partial class ListadoEspecialidades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EspecialidadNegocio especialidad = new EspecialidadNegocio();
                dgvEspecialidad.DataSource = especialidad.Listar();
                dgvEspecialidad.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AMEspecialidad.aspx", false);
        }
    }
}