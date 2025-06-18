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
            bool hayerror=false;

            //validar DNI
            int documento;
            if (string.IsNullOrEmpty(txtDNI.Text))
            {
                lblDNI.ForeColor = System.Drawing.Color.Red;
                lblDNI.Text = "Campo obligatorio";
                txtDNI.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else if (txtDNI.Text.Length > 8)
            {
                lblDNI.ForeColor = System.Drawing.Color.Red;
                lblDNI.Text = "Excediste los caracteres permitidos.";
                txtDNI.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else if (!int.TryParse(txtDNI.Text, out documento)) //controla que el DNI solo tenga numeros
            {
                lblDNI.ForeColor = System.Drawing.Color.Red;
                lblDNI.Text = "El DNI solo debe contener números";
                txtDNI.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else
            {
                lblDNI.ForeColor = System.Drawing.Color.Green;
                lblDNI.Text = "✓ Campo válido.";
                txtDNI.CssClass = "form-control form-control-lg mx-auto is-valid";
            }

            //validar Nombre
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblNombre.ForeColor = System.Drawing.Color.Red;
                lblNombre.Text = "Campo obligatorio";
                txtNombre.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else if (txtNombre.Text.Length > 60)
            {
                lblNombre.ForeColor = System.Drawing.Color.Red;
                lblNombre.Text = "Excediste los caracteres permitidos.";
                txtNombre.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else
            {
                lblNombre.ForeColor = System.Drawing.Color.Green;
                lblNombre.Text = "✓ Campo válido.";
                txtNombre.CssClass = "form-control form-control-lg mx-auto is-valid";
            }
            //validar Apellido
            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                lblApellido.ForeColor = System.Drawing.Color.Red;
                lblApellido.Text = "Campo obligatorio";
                txtApellido.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else if (txtApellido.Text.Length > 60)
            {
                lblApellido.ForeColor = System.Drawing.Color.Red;
                lblApellido.Text = "Excediste los caracteres permitidos.";
                txtApellido.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else
            {
                lblApellido.ForeColor = System.Drawing.Color.Green;
                lblApellido.Text = "✓ Campo válido.";
                txtApellido.CssClass = "form-control form-control-lg mx-auto is-valid";
            }
            //validar FechaNac
            if (string.IsNullOrEmpty(txtFechaNac.Text))
            {
                lblFechaNac.ForeColor = System.Drawing.Color.Red;
                lblFechaNac.Text = "Campo obligatorio";
                txtFechaNac.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else
            {
                DateTime fechaNacimiento;

                if (DateTime.TryParse(txtFechaNac.Text, out fechaNacimiento))
                {
                    if (fechaNacimiento.Year < 1905)
                    {
                        lblFechaNac.ForeColor = System.Drawing.Color.Red;
                        lblFechaNac.Text = "El año no puede ser menor a 1905";
                        txtFechaNac.CssClass = "form-control form-control-lg mx-auto is-invalid";
                        hayerror = true;
                    }
                    else
                    {
                        lblFechaNac.ForeColor = System.Drawing.Color.Green;
                        lblFechaNac.Text = "✓ Campo válido.";
                        txtFechaNac.CssClass = "form-control form-control-lg mx-auto is-valid";
                    }
                }
                else
                {
                    lblFechaNac.ForeColor = System.Drawing.Color.Red;
                    lblFechaNac.Text = "Formato de fecha inválido.";
                    txtFechaNac.CssClass = "form-control form-control-lg mx-auto is-invalid";
                    hayerror = true;
                }
            }

            //validar Telefono
            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                lblTelefono.ForeColor = System.Drawing.Color.Red;
                lblTelefono.Text = "Campo obligatorio";
                txtTelefono.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else if (txtTelefono.Text.Length > 15)
            {
                lblTelefono.ForeColor = System.Drawing.Color.Red;
                lblTelefono.Text = "Excediste los caracteres permitidos.";
                txtTelefono.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else
            {
                lblTelefono.ForeColor = System.Drawing.Color.Green;
                lblTelefono.Text = "✓ Campo válido.";
                txtTelefono.CssClass = "form-control form-control-lg mx-auto is-valid";
            }

            //validar Direccion
            if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                lblDireccion.ForeColor = System.Drawing.Color.Red;
                lblDireccion.Text = "Campo obligatorio";
                txtDireccion.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else if (txtDireccion.Text.Length > 60)
            {
                lblDireccion.ForeColor = System.Drawing.Color.Red;
                lblDireccion.Text = "Excediste los caracteres permitidos.";
                txtDireccion.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else
            {
                lblDireccion.ForeColor = System.Drawing.Color.Green;
                lblDireccion.Text = "✓ Campo válido.";
                txtDireccion.CssClass = "form-control form-control-lg mx-auto is-valid";
            }

            //validar Email
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                lblEmail.ForeColor = System.Drawing.Color.Red;
                lblEmail.Text = "Campo obligatorio";
                txtEmail.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else if (txtEmail.Text.Length > 100)
            {
                lblEmail.ForeColor = System.Drawing.Color.Red;
                lblEmail.Text = "Excediste los caracteres permitidos.";
                txtEmail.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains(".com"))
            {
                lblEmail.ForeColor = System.Drawing.Color.Red;
                lblEmail.Text = "Formato de mail incorreto.";
                txtEmail.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else
            {
                lblEmail.ForeColor = System.Drawing.Color.Green;
                lblEmail.Text = "✓ Campo válido.";
                txtEmail.CssClass = "form-control form-control-lg mx-auto is-valid";
            }

            if (hayerror)
            {
                return;
            }

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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", "alert('¡Registro exitoso!');", true);
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", "alert('¡Datos actualizados!');", true);
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
            else
            {
                lblDNI.ForeColor = System.Drawing.Color.Red;
                lblDNI.Text = "DNI no encontrado";
                txtDNI.CssClass = "form-control form-control-lg mx-auto is-invalid";
            }
        }
    }
}