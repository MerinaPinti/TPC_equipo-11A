using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            }
        }

        protected void btnAgregarEspecialiad_Click(object sender, EventArgs e)
        {
            List<Especialidad> especialidades = (List<Especialidad>)Session["especialidades"];
            especialidades.Add(new Especialidad { Nombre = txtEspecialidad.Text });
            dgvEspecialidades.DataSource = especialidades;
            dgvEspecialidades.DataBind();
            txtEspecialidad.Text = null;
            Session["especialidades"] = especialidades;
            btnContinuar.Visible = true;

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
            Response.Redirect("EleccionTurno.aspx");
        }
    }
}