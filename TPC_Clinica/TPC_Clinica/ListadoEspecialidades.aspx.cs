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
            Usuario usuario = (Usuario)Session["usuario"] != null ? (Usuario)Session["usuario"] : null;
            if (usuario == null || usuario.TipoUsuario.Id == 3)
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
            Session.Remove("IdModificarEspecialidad");
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
                Session.Add("IdModificarEspecialidad", id);
                Response.Redirect("AltaEspecialidad.aspx", true);
            }
        }

        protected void dgvEspecialidades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            EspecialidadNegocio negocio = new EspecialidadNegocio();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int tipoUsuario = ((Usuario)Session["usuario"]).TipoUsuario.Id;

                ImageButton btnModificar = (ImageButton)e.Row.FindControl("btnModificar");
                ImageButton btnEliminar = (ImageButton)e.Row.FindControl("btnEliminar");
                Especialidad especialidad = (Especialidad)e.Row.DataItem;

                if (negocio.enUso(especialidad.Id))
                {
                    btnEliminar.CommandName = "";
                    btnEliminar.CssClass = "transparente";
                    btnEliminar.OnClientClick = "return alert('No se puede eliminar esta especialidad ya que se encuentra en uso');";
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
