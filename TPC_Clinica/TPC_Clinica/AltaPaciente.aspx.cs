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
    public partial class AltaPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGuardarPaciente_Click(object sender, EventArgs e)
        {
            PacienteNegocio negocio = new PacienteNegocio();

            try
            {
                Paciente nuevo = new Paciente();

                nuevo.DNI = txtDNI.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Apellido = txtApellido.Text;
                nuevo.Fecha_Nacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                nuevo.Telefono = txtTelefono.Text;
                nuevo.Email = txtEmail.Text;
                nuevo.Direccion = txtDireccion.Text;
                nuevo.NroHC = Int32.Parse(txtHistoriaClinica.Text);


                negocio.agregarPaciente(nuevo);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}