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
    public partial class AltaUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Usuario> usuarios = new List<Usuario>();
                Session.Add("usuarios", usuarios);

                if (Session["IdModificarUsuario"] != null)
                {
                    int id = (int)Session["IdModificarUsuario"];
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    Usuario modificar = negocio.ListarConId(id);

                    lblIdMod.Visible = true;
                    txtBoxIdMod.Visible = true;
                    txtBoxIdMod.Text = modificar.Id.ToString();
                    txtUsuario.Text = modificar.UserName;
                    txtPassword.Text = modificar.Password;
                    txtTipoUsuario.Text = modificar.TipoUsuario;
                    btnAgregarUsuario.Text = "Modificar";
                }
            }
        }

        protected void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            if (Session["IdModificarUsuario"] == null)
            {
                List<Usuario> usuarios = (List<Usuario>)Session["usuarios"];
                usuarios.Add(new Usuario
                {
                    UserName = txtUsuario.Text,
                    Password = txtPassword.Text,
                    TipoUsuario = txtTipoUsuario.Text
                });
                dgvUsuarios.DataSource = usuarios;
                dgvUsuarios.DataBind();
                txtUsuario.Text = null;
                txtPassword.Text = null;
                txtTipoUsuario.Text = null;
                Session["usuarios"] = usuarios;
                btnContinuar.Visible = true;
            }
            else
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario modificar = new Usuario { Password = txtPassword.Text, Id = Convert.ToInt32(txtBoxIdMod.Text) };
                negocio.modificarUsuario(modificar);
                Response.Redirect("ListadoUsuarios.aspx", true);
            }

        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            negocio.agregarUsuario((List<Usuario>)Session["usuarios"]);
            Session.Remove("idModificarUsuario");
            Response.Redirect("ListadoUsuarios.aspx");
        }


        protected void dgvUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           
        }
    }
}