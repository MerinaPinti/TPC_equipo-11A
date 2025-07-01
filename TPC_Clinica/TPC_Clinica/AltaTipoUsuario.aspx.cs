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
    public partial class AltaTipoUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((string)Session["usuario"]))
            {
                Session["error"] = "Debe iniciar sesión para acceder a esta página.";
                Response.Redirect("Error.aspx", false);
            }

            if (!IsPostBack)
            {

                List<TipoUsuario> listaTiposUsuario = new List<TipoUsuario>();
                Session.Add("listaTiposUsuario", listaTiposUsuario);

                if (Session["IdModificarTipoUsuario"] != null)
                {
                    int id = (int)Session["IdModificarTipoUsuario"];

                    TipoUsuarioNegocio negocio = new TipoUsuarioNegocio();
                    TipoUsuario modificar = negocio.ListarConId(id);

                    lblIdModTipo.Visible = true;
                    txtBoxIdModTipo.Visible = true;
                    txtBoxIdModTipo.Text = modificar.Id.ToString();
                    txtTipoUsuario.Text = modificar.Descripcion.ToString();

                    btnAgregarTipoUsuario.Text = "Modificar";
                }
            }
        }

        protected void btnAgregarTipoUsuario_Click(object sender, EventArgs e)
        {
            if (!validar()) { return; }

            if (Session["IdModificarTipoUsuario"] == null)
            {
                List<TipoUsuario> listaTiposUsuario = (List<TipoUsuario>)Session["listaTiposUsuario"];
                listaTiposUsuario.Add(new TipoUsuario { Descripcion = txtTipoUsuario.Text });

                dgvTiposUsuario.DataSource = listaTiposUsuario;
                dgvTiposUsuario.DataBind();
                txtTipoUsuario.Text = null;
                Session["listaTiposUsuario"] = listaTiposUsuario;
                lblValidacion.Text = null;
                txtTipoUsuario.CssClass = "form-control";
                btnContinuar.Visible = true;
            }
            else
            {
                TipoUsuarioNegocio negocio = new TipoUsuarioNegocio();
                TipoUsuario modificar = new TipoUsuario { Descripcion = txtTipoUsuario.Text, Id = Convert.ToInt32(txtBoxIdModTipo.Text) };
                negocio.modificarTipoUsuario(modificar);
                Response.Redirect("ListadoTipoUsuario.aspx", true);
            }
        }

        protected void dgvTiposUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccionad = dgvTiposUsuario.SelectedRow.Cells[0].Text;
        }

        protected void dgvTiposUsuario_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<TipoUsuario> listaTiposUsuario = (List<TipoUsuario>)Session["listaTiposUsuario"];
            listaTiposUsuario.RemoveAt(e.RowIndex);

            dgvTiposUsuario.DataSource = listaTiposUsuario;
            dgvTiposUsuario.DataBind();
            Session["listaTiposUsuario"] = listaTiposUsuario;
            if (listaTiposUsuario.Count == 0)
            {
                btnContinuar.Visible = false;
            }
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            TipoUsuarioNegocio negocio = new TipoUsuarioNegocio();
            negocio.agregarTipoUsuario((List<TipoUsuario>)Session["listaTiposUsuario"]);
            Session.Remove("listaTiposUsuario");
            Response.Redirect("ListadoTipoUsuario.aspx");
        }

        protected bool validar()
        {
            if (String.IsNullOrWhiteSpace(txtTipoUsuario.Text))
            {
                lblValidacion.Text = "Por favor ingrese una especialidad";
                txtTipoUsuario.CssClass = "form-control is-invalid";
                lblValidacion.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            if (txtTipoUsuario.Text.Length > 50)
            {
                lblValidacion.Text = "Máximo 50 caractéres";
                txtTipoUsuario.CssClass = "form-control is-invalid";
                lblValidacion.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            return true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoTipoUsuario.aspx", false);
        }

    }
}