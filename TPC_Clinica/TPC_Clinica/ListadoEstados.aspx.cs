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
    public partial class ListadoEstados : System.Web.UI.Page
    {
        private EstadoNegocio Negocio = new EstadoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"] != null ? (Usuario)Session["usuario"] : null;
            if (usuario == null || usuario.TipoUsuario.Id == 3 || usuario.TipoUsuario.Id == 2)
            {
                Session["error"] = "No tiene permisos para acceder a esta página.";
                Response.Redirect("Error.aspx", true);
            }
            Session["paginaAnterior"] = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

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