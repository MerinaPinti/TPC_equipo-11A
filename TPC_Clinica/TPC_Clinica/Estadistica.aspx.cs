using Dominio;
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
    // Página de estadísticas
    public partial class Estadistica : System.Web.UI.Page
    {
        // Variables que se convertirán en scripts JavaScript para poder renderizar los gráficos (torta y barras)
        public string jsonEstados;
        public string jsonEspecialidades;

        // Texto para el médico con más pacientes atendidos
        public string MedicoMasCierresTexto { get; set; }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                // Carga y calcula los datos
                CargarDatos();

                // Envía los datos en js a la pág 
                litEstados.Text = $"<script>const dataEstados = {jsonEstados};</script>";
                litEspecialidades.Text = $"<script>const dataEspecialidades = {jsonEspecialidades};</script>";

                // Envía el texto de médico con más pacientes atendidos
                litMedicoMasCierres.Text = MedicoMasCierresTexto;
            }
        }

        
        private void CargarDatos()
        {
            
            TurnoNegocio negocio = new TurnoNegocio();

            
            List<Turno> turnos = negocio.ListarTurnos();

            // Nombre del doc + cantidad de turnos atendidos 
            (string NombreMedico, int Cantidad) resultado = negocio.ObtenerMedicoConMasTurnosCerrados();

            
            MedicoMasCierresTexto = $"El médico con más turnos cerrados es <strong>{resultado.NombreMedico}</strong> con <strong>{resultado.Cantidad}</strong> turnos.";

            // Agrupa los turnos por estado y cuenta la cantidad de cada uno
            Dictionary<string, int> estados = turnos
                .GroupBy(t => t.Estado?.Descripcion ?? "Sin Estado")
                .ToDictionary(g => g.Key, g => g.Count());

            // Agrupa los turnos por especialidad y cuenta la cantidad de cada una
            Dictionary<string, int> especialidades = turnos
                .GroupBy(t => t.Especialidad?.Descripcion ?? "Sin Especialidad")
                .ToDictionary(g => g.Key, g => g.Count());

            // Envía la información para "serializar" convierte objetos C# en cadenas JSON 
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            jsonEstados = serializer.Serialize(estados);
            jsonEspecialidades = serializer.Serialize(especialidades);
        }
    }
}

