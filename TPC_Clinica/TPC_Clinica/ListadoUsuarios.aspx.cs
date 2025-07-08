using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Clinica
{
    public partial class ListadoUsuarios : System.Web.UI.Page
    {
        private UsuarioNegocio Negocio = new UsuarioNegocio();
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
                dgvUsuarios.DataSource = Negocio.ListarActivos();
                dgvUsuarios.DataBind();
            }
        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Session.Remove("IdModificarUsuario");
            Response.Redirect("AltaUsuario.aspx", true);

        }


        protected void dgvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(dgvUsuarios.DataKeys[index].Value);

            if (e.CommandName == "Eliminar")
            {
                Negocio.EliminarUsuario(Negocio.ListarConId(id));
                dgvUsuarios.DataSource = Negocio.ListarActivos();
                dgvUsuarios.DataBind();
            }

            if (e.CommandName == "Editar")
            {
                Session.Add("IdModificarUsuario", id);
                Response.Redirect("AltaUsuario.aspx", true);
            }
        }

    }
}
