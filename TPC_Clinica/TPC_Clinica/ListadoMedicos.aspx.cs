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
    public partial class ListadoMedicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"] != null ? (Usuario)Session["usuario"] : null;
            if (usuario == null || usuario.TipoUsuario.Id == 3)
            {
                Session["error"] = "No tiene permiso para acceder a esta página.";
                Response.Redirect("Error.aspx", true);
            }
            Session["paginaAnterior"] = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

            if (!IsPostBack)
            {
                MedicoNegocio negocio = new MedicoNegocio();
                gvMedico.DataSource = negocio.listar();
                gvMedico.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Session.Remove("IdModificarMedico");
            Response.Redirect("AltaMedico.aspx");
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvMedico.DataKeys[index].Value);

            if (e.CommandName == "Eliminar")
            {
                MedicoNegocio negocio = new MedicoNegocio();
                negocio.eliminarMedico(id);

                // RECARGA EL GRID
                gvMedico.DataSource = negocio.listar();
                gvMedico.DataBind();
            }

            if (e.CommandName == "Editar")
            {
                Session["IdModificarMedico"] = id;
                Response.Redirect("AltaMedico.aspx", true);
            }
        }
    }
}
