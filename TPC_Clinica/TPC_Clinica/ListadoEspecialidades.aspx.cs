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
                dgvEspecialidades.DataSource = especialidad.Listar();
                dgvEspecialidades.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Session.Remove("IdModificar");
            Response.Redirect("AltaEspecialidad.aspx", true);
        }

        protected void dgvEspecialidades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(dgvEspecialidades.DataKeys[index].Value);

            if (e.CommandName == "Eliminar")
            {
                Label1.Text = "Eliminando: " + id;

            }

            if (e.CommandName == "Editar")
            {
                Session.Add("IdModificar", id);
                Response.Redirect("AltaEspecialidad.aspx", true);
            }
        }
    }
}