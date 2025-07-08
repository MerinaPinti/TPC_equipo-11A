using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Clinica
{
    public partial class AltaUsuario : System.Web.UI.Page
    {
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
                    lblCambiarContraseña.Visible = true;
                    txtPassword.Enabled = false;
                    cboxPassword.Visible = true;
                    ddlTipoUsuario.SelectedValue = modificar.TipoUsuario.Id.ToString();
                    btnAgregarUsuario.Text = "Modificar";
                }
            }
        }

        protected void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            if (Session["IdModificarUsuario"] == null)
            {
                if (!validar())
                {
                    return;
                }

                List<Usuario> usuarios = (List<Usuario>)Session["usuarios"];
                usuarios.Add(new Usuario
                {
                    UserName = txtUsuario.Text.Trim(),
                    Password = txtPassword.Text,
                    TipoUsuario = new TipoUsuario { Id = Convert.ToInt32(ddlTipoUsuario.SelectedValue), Descripcion = ddlTipoUsuario.SelectedItem.Text }
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
                    modificar = new Usuario { Password = txtPassword.Text, Id = Convert.ToInt32(txtBoxIdMod.Text), UserName = txtUsuario.Text };
                }
                else
                {
                    string pass = negocio.ListarConId((int)Session["IdModificarUsuario"]).Password;
                    modificar = new Usuario { Password = pass, Id = Convert.ToInt32(txtBoxIdMod.Text), UserName = txtUsuario.Text };
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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoUsuarios.aspx", false);
        }

        protected void cboxPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxPassword.Checked)
            {
                txtPassword.Enabled = true;
            }
            else txtPassword.Enabled = false;
        }

        protected bool validar()
        {
            bool validator = true;
            Regex regex = new Regex("^[a-zA-Z0-9._]+$");
            //validaciones usuario
            if (txtUsuario.Text.Length > 15)
            {
                lblUsuarioError.ForeColor = System.Drawing.Color.Red;
                lblUsuarioError.Text = "No debe superar los 15 caracteres.";
                txtUsuario.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
            }
            else if (txtUsuario.Text.Length < 4)
            {
                lblUsuarioError.ForeColor = System.Drawing.Color.Red;
                lblUsuarioError.Text = "Debe tener al menos de 4 caracteres.";
                txtUsuario.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
            }
            else if (!regex.IsMatch(txtUsuario.Text))
            {
                lblUsuarioError.ForeColor = System.Drawing.Color.Red;
                lblUsuarioError.Text = "Solo puede contener letras, números, puntos y guiones bajos.";
                txtUsuario.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
            }
            else if (txtUsuario.Text.All(char.IsDigit))
            {
                lblUsuarioError.ForeColor = System.Drawing.Color.Red;
                lblUsuarioError.Text = "El usuario no puede ser solo numérico.";
                txtUsuario.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
            }
            else
            {
                lblUsuarioError.ForeColor = System.Drawing.Color.Green;
                txtUsuario.CssClass = "form-control form-control-lg mx-auto is-valid";
                lblUsuarioError.Text = "Campo válido.";
            }

            //Validaciones constraseña
            if (txtPassword.Text.Length < 8)
            {
                lblContraseñaError.ForeColor = System.Drawing.Color.Red;
                lblContraseñaError.Text = "Debe tener al menos 8 caracteres.";
                txtPassword.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
            }
            else if (txtPassword.Text.Length > 255)
            {
                lblContraseñaError.ForeColor = System.Drawing.Color.Red;
                lblContraseñaError.Text = "No debe superar los 255 caracteres.";
                txtPassword.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
            }
            else
            {
                lblContraseñaError.ForeColor = System.Drawing.Color.Green;
                txtPassword.CssClass = "form-control form-control-lg mx-auto is-valid";
                lblContraseñaError.Text = "Campo válido.";
            }
            return validator;
        }
    }
}