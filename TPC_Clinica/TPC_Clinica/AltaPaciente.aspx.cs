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
            Paciente nuevo = negocio.existePaciente(txtDNI.Text);

            try
            {
                if (nuevo == null)
                {
                    nuevo = new Paciente();

                    nuevo.DNI = txtDNI.Text;
                    nuevo.Nombre = txtNombre.Text;
                    nuevo.Apellido = txtApellido.Text;
                    nuevo.FechaNac = DateTime.Parse(txtFechaNac.Text);
                    nuevo.Telefono = txtTelefono.Text;
                    nuevo.Email = txtEmail.Text;
                    nuevo.Direccion = txtDireccion.Text;


                    negocio.agregarPaciente(nuevo);
                }
                else
                {

                    nuevo.Nombre = txtNombre.Text;
                    nuevo.Apellido = txtApellido.Text;
                    nuevo.FechaNac = DateTime.Parse(txtFechaNac.Text);
                    nuevo.Telefono = txtTelefono.Text;
                    nuevo.Email = txtEmail.Text;
                    nuevo.Direccion = txtDireccion.Text;

                    negocio.modificarPaciente(nuevo);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            PacienteNegocio negocio = new PacienteNegocio();
            Paciente paciente = new Paciente();

            paciente = negocio.existePaciente(txtDNI.Text);

            if (paciente != null)
            {
                txtNombre.Text = paciente.Nombre;
                txtApellido.Text = paciente.Apellido;
                txtFechaNac.Text = paciente.FechaNac.ToString("yyyy-MM-dd");
                txtTelefono.Text = paciente.Telefono;
                txtEmail.Text = paciente.Email;
                txtDireccion.Text = paciente.Direccion;

            }
        }
    }
}