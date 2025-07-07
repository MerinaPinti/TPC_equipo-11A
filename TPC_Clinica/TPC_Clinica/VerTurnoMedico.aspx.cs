using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Clinica
{
    public partial class VerTurnoMedico : System.Web.UI.Page
    {

        private int idMedico;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtener el médico logueado
                idMedico = ((Medico)Session["medico"]).IdMedico;

                // Cargar especialidades del médico
                EspecialidadNegocio negocio = new EspecialidadNegocio();
                ddlEspecialidades.DataSource = negocio.ListarPorMedico(idMedico);
                ddlEspecialidades.DataTextField = "Descripcion";
                ddlEspecialidades.DataValueField = "Id";
                ddlEspecialidades.DataBind();

                ddlEspecialidades.Items.Insert(0, new ListItem("-- Seleccione Especialidad --", ""));
            }
        }

        protected void ddlEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idEspecialidad = int.Parse(ddlEspecialidades.SelectedValue);
            TurnoNegocio negocio = new TurnoNegocio();
            gvTurnos.DataSource = negocio.ListarTurnosAsignadosSemana(idMedico, idEspecialidad);
            gvTurnos.DataBind();
        }

        protected void gvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvTurnos.Rows[index];
            string nroTurno = gvTurnos.DataKeys[index].Value.ToString();

            if (e.CommandName == "Atender")
            {
                // Redirigir a página de atención
                Response.Redirect("todavía.aspx" + nroTurno);
            }
            else if (e.CommandName == "NoAsistio")
            {
                Turno turno = new Turno
                {
                    NroTurno = nroTurno,
                    Estado = new Estado { Id = 4 } // 4 = No Asistió
                };

                TurnoNegocio negocio = new TurnoNegocio();
                negocio.actualizarTurnoMedico(turno); // pasamos el objeto turno
                ddlEspecialidades_SelectedIndexChanged(null, null); // refrescar grilla
            }
        }
    }
}
