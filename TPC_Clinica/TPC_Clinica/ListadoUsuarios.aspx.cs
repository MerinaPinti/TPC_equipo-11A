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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                dgvUsuarios.DataSource = negocio.Listar();
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

            }

            if (e.CommandName == "Editar")
            {
                Session.Add("IdModificar", id);
                Response.Redirect("AltaUsuario.aspx", true);
            }
        }

    }
    }
