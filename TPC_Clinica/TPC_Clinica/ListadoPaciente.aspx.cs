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
    }
}