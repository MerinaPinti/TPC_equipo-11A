
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using System.ComponentModel;
using System.Xml.Linq;



namespace TPC_Clinica
{
    public partial class AsignarTurno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBuscarPaciente_Click(object sender, EventArgs e)
        {
            string dni = txtDni.Text.Trim();
            if (!string.IsNullOrEmpty(dni))
            {
                // Mostrar el formulario oculto
                panelFormularioTurno.Visible = true;

                // Cargar especialidades solo si aún no está cargado
                if (ddlEspecialidad.Items.Count == 0)
                {
                    EspecialidadesNegocio negocio = new EspecialidadesNegocio();
                    List<Especialidades> lista = negocio.Listar();

                    ddlEspecialidad.DataSource = lista;
                    ddlEspecialidad.DataTextField = "Nombre";
                    ddlEspecialidad.DataValueField = "ID";
                    ddlEspecialidad.DataBind();

                    ddlEspecialidad.Items.Insert(0, new ListItem("-- Seleccione una especialidad --", ""));
                }
            }
        }
    }
}