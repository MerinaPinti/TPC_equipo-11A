using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_Clinica
{
    public partial class AMEspecialidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Especialidad> especialidades = new List<Especialidad>();
                Session.Add("especialidades", especialidades);

                if (Session["IdModificarEspecialidad"] != null)
                {
                    int id = (int)Session["IdModificarEspecialidad"];
                    EspecialidadNegocio negocio = new EspecialidadNegocio();
                    Especialidad modificar = negocio.ListarConId(id);

                    lblIdMod.Visible = true;
                    txtBoxIdMod.Visible = true;
                    txtBoxIdMod.Text = modificar.Id.ToString();
                    txtEspecialidad.Text = modificar.Descripcion.ToString();
                    btnAgregarEspecialiad.Text = "Modificar";
                }
            }
        }

        protected void btnAgregarEspecialiad_Click(object sender, EventArgs e)
        {

            if (!validar()) { return; }

            if (Session["IdModificarEspecialidad"] == null)
            {
                List<Especialidad> especialidades = (List<Especialidad>)Session["especialidades"];
                especialidades.Add(new Especialidad { Descripcion = txtEspecialidad.Text });
                dgvEspecialidades.DataSource = especialidades;
                dgvEspecialidades.DataBind();
                txtEspecialidad.Text = null;
                Session["especialidades"] = especialidades;
                lblValidacion.Text = null;
                txtEspecialidad.CssClass = "form-control";
                btnContinuar.Visible = true;
            }
            else
            {
                EspecialidadNegocio negocio = new EspecialidadNegocio();
                Especialidad modificar = new Especialidad { Descripcion = txtEspecialidad.Text, Id = Convert.ToInt32(txtBoxIdMod.Text) };
                negocio.modificarEspecialidad(modificar);
                Response.Redirect("ListadoEspecialidades.aspx", true);
            }


        }

        protected void dgvEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccionad = dgvEspecialidades.SelectedRow.Cells[0].Text;
        }

        protected void dgvEspecialidades_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<Especialidad> especialidades = (List<Especialidad>)Session["especialidades"];
            especialidades.RemoveAt(e.RowIndex);
            dgvEspecialidades.DataSource = especialidades;
            dgvEspecialidades.DataBind();
            Session["especialidades"] = especialidades;
            if (especialidades.Count == 0)
            {
                btnContinuar.Visible = false;
            }
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            EspecialidadNegocio negocio = new EspecialidadNegocio();
            negocio.agregarEspecialidad((List<Especialidad>)Session["especialidades"]);
            Session.Remove("especialidades");
            Response.Redirect("ListadoEspecialidades.aspx");
        }

        protected bool validar()
        {
            if (String.IsNullOrEmpty(txtEspecialidad.Text))
            {
                lblValidacion.Text = "Por favor ingrese una especialidad";
                txtEspecialidad.CssClass = "form-control is-invalid";
                lblValidacion.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            if (txtEspecialidad.Text.Length > 50)
            {
                lblValidacion.Text = "Máximo 50 caractéres";
                txtEspecialidad.CssClass = "form-control is-invalid";
                lblValidacion.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            return true;
        }
    }
}
