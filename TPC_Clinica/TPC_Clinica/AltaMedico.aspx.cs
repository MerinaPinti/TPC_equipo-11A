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
            EspecialidadNegocio negocio = new EspecialidadNegocio();
            List<Dominio.Especialidad> lista = negocio.Listar();

            ddlEspecialidad.DataSource = lista;
            ddlEspecialidad.DataTextField = "Descripcion";
            ddlEspecialidad.DataValueField = "ID";
            ddlEspecialidad.DataBind();

            ddlEspecialidad.Items.Insert(0, new ListItem("-- Seleccione una especialidad --", ""));

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
           /* MedicoNegocio negocio = new MedicoNegocio();
            Medico nuevo = negocio.existeMedico(txtMatricula.Text);

            try
            {
                if (nuevo== null) 
                { 
                    nuevo = new Medico();

                    nuevo.Matricula = txtMatricula.Text;
                    nuevo.Nombre = txtNombre.Text;
                    nuevo.Apellido = txtApellido.Text;
                    //nuevo.Especialidad = ;
                    nuevo.Email = txtEmail.Text;
                    nuevo.Telefono = txtTelefono.Text;

                    negocio.agregarMedico(nuevo);
                }
                else
                {
                    
                    nuevo.Nombre = txtNombre.Text;
                    nuevo.Apellido = txtApellido.Text;
                    //nuevo.Especialidad = 
                    nuevo.Telefono = txtTelefono.Text;
                    nuevo.Email = txtEmail.Text;

                    negocio.modificarMedico(nuevo);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           */
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
    }
}