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
            if (String.IsNullOrEmpty(txtTipoUsuario.Text))
            {
                lblValidacionTipo.Text = "Por favor ingrese un Tipo de Usuario";
                txtTipoUsuario.CssClass = "form-control is-invalid";
                lblValidacionTipo.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (txtTipoUsuario.Text.Length > 50)
            {
                lblValidacionTipo.Text = "El campo debe tener máximo 50 caracteres";
                txtTipoUsuario.CssClass = "form-control is-invalid";
                lblValidacionTipo.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (Session["IdModificarTipoUsuario"] == null)
            {
                List<TipoUsuario> listaTiposUsuario = (List<TipoUsuario>)Session["listaTiposUsuario"];
                listaTiposUsuario.Add(new TipoUsuario { Descripcion = txtTipoUsuario.Text });

                dgvTiposUsuario.DataSource = listaTiposUsuario;
                dgvTiposUsuario.DataBind();
                txtTipoUsuario.Text = null;
                Session["listaTiposUsuario"] = listaTiposUsuario;
                lblValidacionTipo.Text = null;
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
    }
}