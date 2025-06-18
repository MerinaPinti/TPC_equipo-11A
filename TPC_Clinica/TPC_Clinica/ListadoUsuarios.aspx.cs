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
            if (!IsPostBack)
            {
                dgvUsuarios.DataSource = Negocio.ListarActivos();
                dgvUsuarios.DataBind();
            }
        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Session.Remove("IdModificar");
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
