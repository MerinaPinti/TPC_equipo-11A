using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Clinica
{
    public partial class ListadoPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarPacientes();
            }
        }

        private void cargarPacientes()
        {
            PacienteNegocio negocio = new PacienteNegocio();
            dgvPacientes.DataSource = negocio.listarPacientes();
            dgvPacientes.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Session.Remove("DniModificarPaciente");
            Response.Redirect("AltaPaciente.aspx", true);
        }

        protected void dgvPacientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string dni = dgvPacientes.DataKeys[index].Value.ToString();

            PacienteNegocio negocio = new PacienteNegocio();

            if (e.CommandName == "Eliminar")
            {
                negocio.eliminarLogico(dni);
                cargarPacientes();
            }

            if (e.CommandName == "Editar")
            {
                Session.Add("DniModificarPaciente", dni);
                Response.Redirect("AltaPaciente.aspx", true);
            }
        }
    }
}