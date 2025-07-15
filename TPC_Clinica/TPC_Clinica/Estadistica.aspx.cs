using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Clinica
{
    public partial class Estadistica : System.Web.UI.Page
    {
        public string jsonEstados;
        public string jsonEspecialidades;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();

                
                litEstados.Text = $"<script>const dataEstados = {jsonEstados};</script>";
                litEspecialidades.Text = $"<script>const dataEspecialidades = {jsonEspecialidades};</script>";
            }
        }

        private void CargarDatos()
        {
            TurnoNegocio negocio = new TurnoNegocio();
            var turnos = negocio.ListarTurnos();

            var estados = turnos
                .GroupBy(t => t.Estado?.Descripcion ?? "Sin Estado")
                .ToDictionary(g => g.Key, g => g.Count());

            var especialidades = turnos
                .GroupBy(t => t.Especialidad?.Descripcion ?? "Sin Especialidad")
                .ToDictionary(g => g.Key, g => g.Count());

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            jsonEstados = serializer.Serialize(estados);
            jsonEspecialidades = serializer.Serialize(especialidades);
        }
    }
}

