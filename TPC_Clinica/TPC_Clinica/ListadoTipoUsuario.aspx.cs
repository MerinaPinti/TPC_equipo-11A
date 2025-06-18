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
    }
}