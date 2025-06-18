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
    public partial class AltaTipoUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["IdModificarTipoUsuario"] != null)
            {
                int id = (int)Session["IdModificarTipoUsuario"];
                TipoUsuarioNegocio negocio = new TipoUsuarioNegocio();
                TipoUsuario tipo = negocio.ListarConId(id);
                txtIdTipoUsuario.Text = tipo.Id.ToString();
                txtDescripcionTipoUsuario.Text = tipo.Descripcion;
            }
        }

        protected void btnAgregarTipoUsuario_Click(object sender, EventArgs e)
        {
            TipoUsuario nuevo = new TipoUsuario { Descripcion = txtTipoUsuario.Text };
            TipoUsuarioNegocio negocio = new TipoUsuarioNegocio();
            negocio.agregarTipoUsuario(nuevo);
            Response.Redirect("ListadoTipoUsuario.aspx");
        }

        protected void btnModificarTipoUsuario_Click(object sender, EventArgs e)
        {
            TipoUsuario modificar = new TipoUsuario
            {
                Id = int.Parse(txtIdTipoUsuario.Text),
                Descripcion = txtDescripcionTipoUsuario.Text
            };
            TipoUsuarioNegocio negocio = new TipoUsuarioNegocio();
            negocio.modificarTipoUsuario(modificar);
            Response.Redirect("ListadoTipoUsuario.aspx");
        }
    }
}