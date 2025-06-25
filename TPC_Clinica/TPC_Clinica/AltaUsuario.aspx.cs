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
                TipoUsuarioNegocio negocioUsuario = new TipoUsuarioNegocio();
                List<TipoUsuario> lista = negocioUsuario.Listar();
                ddlTipoUsuario.DataSource = lista;
                ddlTipoUsuario.DataTextField = "Descripcion";
                ddlTipoUsuario.DataValueField = "Id";
                ddlTipoUsuario.DataBind();

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
                    ddlTipoUsuario.SelectedValue = modificar.TipoUsuario.Id.ToString();
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
                    TipoUsuario = new TipoUsuario{Id = Convert.ToInt32(ddlTipoUsuario.SelectedValue), Descripcion = ddlTipoUsuario.SelectedItem.Text}
                });
                Session["usuarios"] = usuarios;
                dgvUsuarios.DataSource = usuarios;
                dgvUsuarios.DataBind();
                txtUsuario.Text = null;
                txtPassword.Text = null;
                ddlTipoUsuario.SelectedIndex = 0;
                btnContinuar.Visible = true;
            }
            else
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario modificar;
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    modificar = new Usuario { Password = txtPassword.Text, Id = Convert.ToInt32(txtBoxIdMod.Text) };
                }
                else
                {
                    string pass = negocio.ListarConId((int)Session["IdModificarUsuario"]).Password;
                    modificar = new Usuario { Password = pass, Id = Convert.ToInt32(txtBoxIdMod.Text) };
                }
                modificar.TipoUsuario = new TipoUsuario { Descripcion = ddlTipoUsuario.SelectedItem.Text, Id = Convert.ToInt32(ddlTipoUsuario.SelectedValue) };
                negocio.modificarUsuario(modificar);
                Response.Redirect("ListadoUsuarios.aspx", true);
            }

        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            negocio.agregarUsuario((List<Usuario>)Session["usuarios"]);
            Session.Remove("idModificarUsuario");
            Response.Redirect("ListadoUsuarios.aspx", true);
        }


        protected void dgvUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           
        }
    }
}