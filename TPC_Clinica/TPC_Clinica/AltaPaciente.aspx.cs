using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Dominio;
using Negocio;

namespace TPC_Clinica
{
    public partial class AltaPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"] != null ? (Usuario)Session["usuario"] : null;
            if (usuario == null || usuario.TipoUsuario.Id == 3)
            {
                Session["error"] = "No tiene permisos para acceder a esta página.";
                Response.Redirect("Error.aspx", true);
            }
            Session["paginaAnterior"] = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

            if (!IsPostBack)
            {
                if (string.IsNullOrWhiteSpace(txtDNI.Text))
                {
                    txtNombre.Text = string.Empty;
                    txtApellido.Text = string.Empty;
                    txtFechaNac.Text = string.Empty;
                    txtTelefono.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtDireccion.Text = string.Empty;

                    lblNombre.Text = "";
                    lblApellido.Text = "";
                    lblFechaNac.Text = "";
                    lblTelefono.Text = "";
                    lblEmail.Text = "";
                    lblDNI.Text = "";
                }

                if (Session["DniModificarPaciente"] != null)
                {
                    string dniRecibido = Session["DniModificarPaciente"].ToString();
                    PacienteNegocio negocio = new PacienteNegocio();
                    Paciente paciente = negocio.existePaciente(dniRecibido);

                    if (paciente != null)
                    {
                        txtDNI.Text = paciente.DNI;
                        txtNombre.Text = paciente.Nombre;
                        txtApellido.Text = paciente.Apellido;
                        txtFechaNac.Text = paciente.FechaNac.ToString("yyyy-MM-dd");
                        txtTelefono.Text = paciente.Telefono;
                        txtEmail.Text = paciente.Email;
                        txtDireccion.Text = paciente.Direccion;
                        txtDNI.Enabled = false;
                        lblDNI.ForeColor = System.Drawing.Color.Blue;
                    }
                }
            }
        }

        protected void btnGuardarPaciente_Click(object sender, EventArgs e)
        {
            PacienteNegocio negocio = new PacienteNegocio();
            Paciente nuevo = negocio.existePaciente(txtDNI.Text);
            if (!validar())
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

                    //------------------------------ENVIO DE MAIL------------------------------
                    string rutaPlantillas = Server.MapPath("~/Templates");

                    var reemplazos = new Dictionary<string, string>
                    {
                        { "NOMBRE", nuevo.Nombre + " " + nuevo.Apellido }
                    };

                    EmailService emailService = new EmailService();
                    emailService.armarCorreo(
                        txtEmail.Text,
                        "Te damos la bienvenida a Clínica Médica Meraki 💙",
                        reemplazos,
                        TipoCorreo.EmailAltaPaciente,
                        rutaPlantillas
                    );
                    emailService.enviarCorreo();
                    //-------------------------------------------------------------------------

                    Response.Redirect("ListadoPaciente.aspx", false);
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

                    //------------------------------ENVIO DE MAIL------------------------------
                    string rutaPlantillas = Server.MapPath("~/Templates");

                    var reemplazos = new Dictionary<string, string>
                    {
                        { "NOMBRE", nuevo.Nombre + " " + nuevo.Apellido }
                    };

                    EmailService emailService = new EmailService();
                    emailService.armarCorreo(
                        txtEmail.Text,
                        "Información actualizada con éxito en Clínica Médica Meraki ✔️",
                        reemplazos,
                        TipoCorreo.EmailModificarPaciente,
                        rutaPlantillas
                    );
                    emailService.enviarCorreo();
                    //-------------------------------------------------------------------------

                    Response.Redirect("ListadoPaciente.aspx", false);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected bool validar()
        {
            int documento;
            bool validator = true;
            PacienteNegocio negocio = new PacienteNegocio();
            Paciente paciente = negocio.existePaciente(txtDNI.Text);


            if (string.IsNullOrWhiteSpace(txtDNI.Text))
            {
                lblDNI.ForeColor = System.Drawing.Color.Red;
                lblDNI.Text = "Campo obligatorio";
                txtDNI.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
            }
            else if (txtDNI.Text.Length > 8)
            {
                lblDNI.ForeColor = System.Drawing.Color.Red;
                lblDNI.Text = "Excediste los caracteres permitidos.";
                txtDNI.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
            }
            else if (!int.TryParse(txtDNI.Text, out documento)) //controla que el DNI solo tenga numeros
            {
                lblDNI.ForeColor = System.Drawing.Color.Red;
                lblDNI.Text = "El DNI solo debe contener números";
                txtDNI.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
            }
            else if (paciente != null && (string)Session["DniModificarPaciente"] == null)
            {
                string hola = (string)Session["DniModificarPaciente"];
                lblDNI.ForeColor = System.Drawing.Color.Red;
                lblDNI.Text = "DNI ya registrado.";
                txtDNI.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
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
                validator = false;
            }
            else if (txtNombre.Text.Length > 60)
            {
                lblNombre.ForeColor = System.Drawing.Color.Red;
                lblNombre.Text = "Excediste los caracteres permitidos.";
                txtNombre.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
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
                validator = false;
            }
            else if (txtApellido.Text.Length > 60)
            {
                lblApellido.ForeColor = System.Drawing.Color.Red;
                lblApellido.Text = "Excediste los caracteres permitidos.";
                txtApellido.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
            }
            else
            {
                lblApellido.ForeColor = System.Drawing.Color.Green;
                lblApellido.Text = "✓ Campo válido.";
                txtApellido.CssClass = "form-control form-control-lg mx-auto is-valid";
            }
            //validar FechaNac
            if (string.IsNullOrWhiteSpace(txtFechaNac.Text))
            {
                lblFechaNac.ForeColor = System.Drawing.Color.Red;
                lblFechaNac.Text = "Campo obligatorio";
                txtFechaNac.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
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
                        validator = false;
                    }
                    else if (fechaNacimiento.Year >= DateTime.Now.Year)
                    {
                        lblFechaNac.ForeColor = System.Drawing.Color.Red;
                        lblFechaNac.Text = "Debe ingresar una fecha válida";
                        txtFechaNac.CssClass = "form-control form-control-lg mx-auto is-invalid";
                        validator = false;
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
                    validator = false;
                }
            }

            //validar Telefono
            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                lblTelefono.ForeColor = System.Drawing.Color.Red;
                lblTelefono.Text = "Campo obligatorio";
                txtTelefono.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
            }
            else if (txtTelefono.Text.Length > 15)
            {
                lblTelefono.ForeColor = System.Drawing.Color.Red;
                lblTelefono.Text = "Excediste los caracteres permitidos.";
                txtTelefono.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
            }
            else
            {
                lblTelefono.ForeColor = System.Drawing.Color.Green;
                lblTelefono.Text = "✓ Campo válido.";
                txtTelefono.CssClass = "form-control form-control-lg mx-auto is-valid";
            }

            //validar Direccion
            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                lblDireccion.ForeColor = System.Drawing.Color.Red;
                lblDireccion.Text = "Campo obligatorio";
                txtDireccion.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
            }
            else if (txtDireccion.Text.Length > 60)
            {
                lblDireccion.ForeColor = System.Drawing.Color.Red;
                lblDireccion.Text = "Excediste los caracteres permitidos.";
                txtDireccion.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
            }
            else
            {
                lblDireccion.ForeColor = System.Drawing.Color.Green;
                lblDireccion.Text = "✓ Campo válido.";
                txtDireccion.CssClass = "form-control form-control-lg mx-auto is-valid";
            }

            //validar Email
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblEmail.ForeColor = System.Drawing.Color.Red;
                lblEmail.Text = "Campo obligatorio";
                txtEmail.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
            }
            else if (txtEmail.Text.Length > 100)
            {
                lblEmail.ForeColor = System.Drawing.Color.Red;
                lblEmail.Text = "Excediste los caracteres permitidos.";
                txtEmail.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
            }
            else if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains(".com"))
            {
                lblEmail.ForeColor = System.Drawing.Color.Red;
                lblEmail.Text = "Formato de mail incorreto.";
                txtEmail.CssClass = "form-control form-control-lg mx-auto is-invalid";
                validator = false;
            }
            else
            {
                lblEmail.ForeColor = System.Drawing.Color.Green;
                lblEmail.Text = "✓ Campo válido.";
                txtEmail.CssClass = "form-control form-control-lg mx-auto is-valid";
            }
            return validator;
        }

        protected void txtDNI_DataBinding(object sender, EventArgs e)
        {
            PacienteNegocio negocio = new PacienteNegocio();
            Paciente paciente = negocio.existePaciente(txtDNI.Text);
            if (paciente != null)
            {
                lblDNI.ForeColor = System.Drawing.Color.Red;
                lblDNI.Text = "DNI ya registrado.";
                txtDNI.CssClass = "form-control form-control-lg mx-auto is-invalid";
            }
            else
            {
                lblDNI.ForeColor = System.Drawing.Color.Green;
                lblDNI.Text = "✓ Campo válido.";
                txtDNI.CssClass = "form-control form-control-lg mx-auto is-valid";
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoPaciente.aspx", false);
        }
    }
}