using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_Clinica
{
    public partial class ListadoEstados : System.Web.UI.Page
    {
        private EstadoNegocio Negocio = new EstadoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((string)Session["usuario"]))
            {
                Session["error"] = "Debe iniciar sesión para acceder a esta página.";
                Response.Redirect("Error.aspx", false);
            }

            if (!IsPostBack)
            {
                dgvEspecialidades.DataSource = Negocio.ListarActivos();
                dgvEspecialidades.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Session.Remove("IdModificarEstado");
            Response.Redirect("AltaEstado.aspx", true);
        }

        protected void dgvEspecialidades_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(dgvEspecialidades.DataKeys[index].Value);

            if (e.CommandName == "Eliminar")
            {
                Negocio.eliminarEstado(Negocio.ListarConId(id));
                dgvEspecialidades.DataSource = Negocio.ListarActivos();
                dgvEspecialidades.DataBind();

            }

            if (e.CommandName == "Editar")
            {
                Session.Add("IdModificarEstado", id);
                Response.Redirect("AltaEstado.aspx", true);
            }
        }
    }
}