using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Clinica
{
    public partial class ListadoMedicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MedicoNegocio negocio = new MedicoNegocio();
                GridView1.DataSource = negocio.listar(); 
                GridView1.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Session.Remove("IdModificarMedico");
            Response.Redirect("AltaMedico.aspx");
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(GridView1.DataKeys[index].Value);

            if (e.CommandName == "Eliminar")
            {
                MedicoNegocio negocio = new MedicoNegocio();
                negocio.eliminarMedico(id); 

                // RECARGA EL GRID
                GridView1.DataSource = negocio.listar();
                GridView1.DataBind();
            }

            if (e.CommandName == "Editar")
            {
                Session["IdModificarMedico"] = id;
                Response.Redirect("AltaMedico.aspx", true);
            }
        }
    }
    }
