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
    public partial class ListadoEspecialidades : System.Web.UI.Page
    {
        private EspecialidadNegocio Negocio = new EspecialidadNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dgvEspecialidades.DataSource = Negocio.ListarActivos();
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
                Negocio.eliminarEspecialidad(Negocio.ListarConId(id));
                dgvEspecialidades.DataSource = Negocio.ListarActivos();
                dgvEspecialidades.DataBind();

            }

            if (e.CommandName == "Editar")
            {
                Session.Add("IdModificar", id);
                Response.Redirect("AltaEspecialidad.aspx", true);
            }
        }
    }
}