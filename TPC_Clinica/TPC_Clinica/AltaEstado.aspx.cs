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
    public partial class AltaEstado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"] != null ? (Usuario)Session["usuario"] : null;
            if (usuario == null || usuario.TipoUsuario.Id == 3 || usuario.TipoUsuario.Id == 2)
            {
                Session["error"] = "No tiene permiso para acceder a esta página.";
                Response.Redirect("Error.aspx", true);
            }
            Session["paginaAnterior"] = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

            if (!IsPostBack)
            {
                List<Estado> estados = new List<Estado>();
                Session.Add("estados", estados);

                if (Session["IdModificarEstado"] != null)
                {
                    int id = (int)Session["IdModificarEstado"];
                    EstadoNegocio negocio = new EstadoNegocio();
                    Estado modificar = negocio.ListarConId(id);

                    lblIdMod.Visible = true;
                    txtBoxIdMod.Visible = true;
                    txtBoxIdMod.Text = modificar.Id.ToString();
                    txtEstado.Text = modificar.Descripcion.ToString();
                    btnAgregarEstado.Text = "Modificar";
                }
            }
        }

        protected void btnAgregarEstado_Click(object sender, EventArgs e)
        {

            if (!validar()) { return; }

            if (Session["IdModificarEstado"] == null)
            {
                List<Estado> Estadoes = (List<Estado>)Session["estados"];
                Estadoes.Add(new Estado { Descripcion = txtEstado.Text });
                dgvEstados.DataSource = Estadoes;
                dgvEstados.DataBind();
                txtEstado.Text = null;
                Session["estados"] = Estadoes;
                lblValidacion.Text = null;
                txtEstado.CssClass = "form-control";
                btnContinuar.Visible = true;
            }
            else
            {
                EstadoNegocio negocio = new EstadoNegocio();
                Estado modificar = new Estado { Descripcion = txtEstado.Text, Id = Convert.ToInt32(txtBoxIdMod.Text) };
                negocio.modificarEstado(modificar);
                Response.Redirect("ListadoEstados.aspx", true);
            }


        }


        protected void dgvEstados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<Estado> Estadoes = (List<Estado>)Session["estados"];
            Estadoes.RemoveAt(e.RowIndex);
            dgvEstados.DataSource = Estadoes;
            dgvEstados.DataBind();
            Session["estados"] = Estadoes;
            if (Estadoes.Count == 0)
            {
                btnContinuar.Visible = false;
            }
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            EstadoNegocio negocio = new EstadoNegocio();
            negocio.agregarEstado((List<Estado>)Session["estados"]);
            Session.Remove("estados");
            Response.Redirect("ListadoEstados.aspx");
        }

        protected bool validar()
        {
            if (String.IsNullOrWhiteSpace(txtEstado.Text))
            {
                lblValidacion.Text = "Por favor ingrese un estado";
                txtEstado.CssClass = "form-control is-invalid";
                lblValidacion.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            if (txtEstado.Text.Length > 50)
            {
                lblValidacion.Text = "Máximo 50 caractéres";
                txtEstado.CssClass = "form-control is-invalid";
                lblValidacion.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            return true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoEstados.aspx", false);
        }
    }
}