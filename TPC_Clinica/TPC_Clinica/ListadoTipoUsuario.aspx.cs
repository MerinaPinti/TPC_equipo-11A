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
    public partial class ListadoTipoUsuario : System.Web.UI.Page
    {
        private TipoUsuarioNegocio Negocio = new TipoUsuarioNegocio();
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
                dgvTiposUsuario.DataSource = Negocio.ListarActivos();
                dgvTiposUsuario.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Session.Remove("IdModificarTipoUsuario");
            Response.Redirect("AltaTipoUsuario.aspx", true);
        }

        protected void dgvTiposUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(dgvTiposUsuario.DataKeys[index].Value);

            if (e.CommandName == "Eliminar")
            {
                Negocio.eliminarTipoUsuario(Negocio.ListarConId(id));

                dgvTiposUsuario.DataSource = Negocio.ListarActivos();
                dgvTiposUsuario.DataBind();
            }

            if (e.CommandName == "Editar")
            {
                Session.Add("IdModificarTipoUsuario", id);

                Response.Redirect("AltaTipoUsuario.aspx", true);
            }
        }

        protected void dgvTiposUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            TipoUsuarioNegocio negocio = new TipoUsuarioNegocio();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int tipoUsuario = ((Usuario)Session["usuario"]).TipoUsuario.Id;

                ImageButton btnModificar = (ImageButton)e.Row.FindControl("btnModificar");
                ImageButton btnEliminar = (ImageButton)e.Row.FindControl("btnEliminar");
                TipoUsuario especialidad = (TipoUsuario)e.Row.DataItem;

                if (negocio.enUso(especialidad.Id))
                {
                    btnEliminar.CommandName = "";
                    btnEliminar.CssClass = "transparente";
                    btnEliminar.OnClientClick = "return alert('No se puede eliminar este Tipo de Usuario ya que, actualmente se encuentra en uso');";
                }

                if (tipoUsuario == 3)
                {
                    if (btnModificar != null) btnModificar.Visible = false;
                    if (btnEliminar != null) btnEliminar.Visible = false;
                }

            }
        }
    }
}
