using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace TPC_Clinica
{
    public partial class AltaMedico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EspecialidadNegocio negocio = new EspecialidadNegocio();
                List<Especialidad> lista = negocio.Listar();
                chkEspecialidades.DataSource = lista;
                chkEspecialidades.DataTextField = "Descripcion";
                chkEspecialidades.DataValueField = "Id";
                chkEspecialidades.DataBind();

                if (Session["IdModificarMedico"] != null)
                {
                    MedicoNegocio negocioMed = new MedicoNegocio();
                    Medico medico = negocioMed.existeMedico((int)Session["IdModificarMedico"]);
                    txtApellido.Text = medico.Apellido;
                    txtEmail.Text = medico.Email;
                    txtMatricula.Text = medico.Matricula;
                    txtMatricula.Enabled = false;
                    txtNombre.Text = medico.Nombre;
                    txtTelefono.Text = medico.Telefono;
                    divMensaje.Visible = false;

                    List<int> idsSeleccionados = new List<int>();
                    foreach (Especialidad obj in medico.Especialidad)
                    {
                        idsSeleccionados.Add(obj.Id);
                    }

                    //List<int> idsSeleccionados = medico.Especialidad.Select(esp => esp.Id).ToList();
                    foreach (ListItem item in chkEspecialidades.Items)
                    {
                        if (idsSeleccionados.Contains(int.Parse(item.Value)))
                        {
                            item.Selected = true;
                        }
                    }

                }
            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool hayerror = false;

            // validar Matricula 
            int matricula;
            if (string.IsNullOrEmpty(txtMatricula.Text))
            {
                lblMatricula.ForeColor = System.Drawing.Color.Red;
                lblMatricula.Text = "Campo obligatorio";
                txtMatricula.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else if (txtMatricula.Text.Length > 8)
            {
                lblMatricula.ForeColor = System.Drawing.Color.Red;
                lblMatricula.Text = "Excediste los caracteres permitidos.";
                txtMatricula.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else if (!int.TryParse(txtMatricula.Text, out matricula))
            {
                lblMatricula.ForeColor = System.Drawing.Color.Red;
                lblMatricula.Text = "La Matricula solo debe contener números";
                txtMatricula.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else
            {
                lblMatricula.ForeColor = System.Drawing.Color.Green;
                lblMatricula.Text = "✓ Campo válido.";
                txtMatricula.CssClass = "form-control form-control-lg mx-auto is-valid";
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

            //validar Especialidad 

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
            else if (!txtTelefono.Text.All(char.IsDigit))
            {
                lblTelefono.ForeColor = System.Drawing.Color.Red;
                lblTelefono.Text = "El teléfono solo debe contener números.";
                txtTelefono.CssClass = "form-control form-control-lg mx-auto is-invalid";
                hayerror = true;
            }
            else
            {
                lblTelefono.ForeColor = System.Drawing.Color.Green;
                lblTelefono.Text = "✓ Campo válido.";
                txtTelefono.CssClass = "form-control form-control-lg mx-auto is-valid";
            }

            if (hayerror)
            {
                return;
            }

            if (Session["idModificarMedico"] == null)
            {
                List<Especialidad> seleccionadas = new List<Especialidad>();
                foreach (ListItem item in chkEspecialidades.Items)
                {
                    if (item.Selected)
                    {
                        seleccionadas.Add(new Especialidad
                        {
                            Id = int.Parse(item.Value),
                            Descripcion = item.Text
                        });
                    }
                }
                Medico nuevoMedico = new Medico();
                nuevoMedico.Especialidad = seleccionadas;
                nuevoMedico.Matricula = txtMatricula.Text;
                nuevoMedico.Apellido = txtApellido.Text;
                nuevoMedico.Nombre = txtNombre.Text;
                nuevoMedico.Email = txtEmail.Text;
                nuevoMedico.Telefono = txtTelefono.Text;

                MedicoNegocio negocio = new MedicoNegocio();
                negocio.agregarMedico(nuevoMedico);
                Response.Redirect("ListadoMedicos.aspx", true);
                                
            }
            else
            {
                MedicoNegocio negocioMed = new MedicoNegocio();
                Medico medico = new Medico();
                medico.IdMedico = negocioMed.existeMedico((int)Session["IdModificarMedico"]).IdMedico;
                medico.Apellido = txtApellido.Text;
                medico.Nombre = txtNombre.Text;
                medico.Email = txtEmail.Text;
                medico.Telefono = txtTelefono.Text;
                medico.Matricula = txtMatricula.Text;
                medico.Especialidad = new List<Especialidad>();

                foreach (ListItem item in chkEspecialidades.Items)
                {
                    if (item.Selected)
                    {
                        medico.Especialidad.Add(new Especialidad
                        {
                            Id = int.Parse(item.Value),
                            Descripcion = item.Text
                        });
                    }
                }
                negocioMed.modificarMedico(medico);
                Response.Redirect("ListadoMedicos.aspx",true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            MedicoNegocio negocio = new MedicoNegocio();
            Medico medico = new Medico();

            medico = negocio.existeMedico(txtMatricula.Text);

            if (medico != null)
            {
                txtMatricula.Text = medico.Matricula;
                txtNombre.Text = medico.Nombre;
                txtApellido.Text = medico.Apellido;
                //Especialidad = medico.;
                txtEmail.Text = medico.Email;
                txtTelefono.Text = medico.Telefono;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoMedicos.aspx", false);
        }
    }
}