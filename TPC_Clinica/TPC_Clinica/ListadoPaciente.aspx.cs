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
    public partial class ListadoPaciente : System.Web.UI.Page
    {
        Usuario usuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = (Usuario)Session["usuario"] != null ? (Usuario)Session["usuario"] : null;
            if (usuario == null)
            {
                Session["error"] = "No tiene permisos para acceder a esta página.";
                Response.Redirect("Error.aspx", true);
            }
            Session["paginaAnterior"] = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

            if (!IsPostBack)
            {
                cargarPacientes();
            }
        }

        private void cargarPacientes()
        {
            PacienteNegocio negocio = new PacienteNegocio();
            if (usuario.TipoUsuario.Id == 3)
            {
                btnAgregar.Visible = false;
                Medico medico = (Medico)Session["medico"];
                dgvPacientes.Columns[4].Visible = false;
                dgvPacientes.DataSource = negocio.listarPacientesPorMedico(medico.IdMedico);
                dgvPacientes.DataBind();
            }
            else
            {
                dgvPacientes.DataSource = negocio.listarPacientes();
                dgvPacientes.DataBind();
            }

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Session.Remove("DniModificarPaciente");
            Response.Redirect("AltaPaciente.aspx", true);
        }

        protected void dgvPacientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string dni = dgvPacientes.DataKeys[index].Value.ToString();

            PacienteNegocio negocio = new PacienteNegocio();

            if (e.CommandName == "Eliminar")
            {
                negocio.eliminarLogico(dni);
                cargarPacientes();
            }

            if (e.CommandName == "Editar")
            {
                Session.Add("DniModificarPaciente", dni);
                Response.Redirect("AltaPaciente.aspx", true);
            }

            if (e.CommandName == "HistoriaClinica")
            {
                Session.Add("DniHistoriaClinica", dni);
                Session.Add("BuscarPorPaciente", true);
                Response.Redirect("HistoriaClinica.aspx", true);
            }
        }

        protected void dgvPacientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            PacienteNegocio negocio = new PacienteNegocio();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int tipoUsuario = ((Usuario)Session["usuario"]).TipoUsuario.Id;

                ImageButton btnModificar = (ImageButton)e.Row.FindControl("btnModificar");
                ImageButton btnEliminar = (ImageButton)e.Row.FindControl("btnEliminar");
                Paciente paciente = (Paciente)e.Row.DataItem;

                if (negocio.enUso(paciente.IdPaciente))
                {
                    btnEliminar.CommandName = "";
                    btnEliminar.CssClass = "transparente";
                    btnEliminar.OnClientClick = "return alert('No se puede eliminar este paciente ya que se encuentra en uso');";
                }

                if (tipoUsuario == 3)
                {
                    if (btnModificar != null) btnModificar.Visible = false;
                    if (btnEliminar != null) btnEliminar.Visible = false;
                }
            }
        }
    }
}
