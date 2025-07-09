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
    public partial class HistoriaClinica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MedicoNegocio medicoNegocio = new MedicoNegocio();
                ddlMedico.DataSource = medicoNegocio.listar();
                ddlMedico.DataTextField = "NombreCompleto";
                ddlMedico.DataValueField = "IdMedico";
                ddlMedico.DataBind();
                ddlMedico.Items.Insert(0, new ListItem("Todos", "0"));

                EspecialidadNegocio espNegocio = new EspecialidadNegocio();
                ddlEspecialidad.DataSource = espNegocio.Listar();
                ddlEspecialidad.DataTextField = "Descripcion";
                ddlEspecialidad.DataValueField = "Id";
                ddlEspecialidad.DataBind();
                ddlEspecialidad.Items.Insert(0, new ListItem("Todas", "0"));

                lblSinTurnos.Visible = false;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string dni = txtDNI.Text.Trim();
            int idMedico = int.Parse(ddlMedico.SelectedValue);
            int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

            TurnoNegocio negocio = new TurnoNegocio();
            var lista = negocio.ListarTurnoPorDNI(dni, 4, idMedico, idEspecialidad);

            if (lista != null && lista.Count > 0)
            {
                gvResultados.DataSource = lista.Select(t => new {
                    Fecha = t.Fecha.ToString("dd/MM/yyyy"),
                    Medico = t.Medico.Nombre,
                    Especialidad = t.Medico.Especialidad?.FirstOrDefault()?.Descripcion ?? "Sin especialidad",
                    Observaciones = t.Observaciones,
                    Diagnostico = t.Diagnostico
                }).ToList();

                gvResultados.DataBind();
                lblSinTurnos.Visible = false;
            }
            else
            {
                gvResultados.DataSource = null;
                gvResultados.DataBind();
                lblSinTurnos.Visible = true;
            }
        }
    }
}
