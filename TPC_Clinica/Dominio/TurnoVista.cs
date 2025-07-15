using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{

    //CLASE AUXILIAR PARA VER LOS TURNOS . 
    public class TurnoVista
    {
        public string NroTurno { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }

        // Paciente
        public string NombrePaciente { get; set; }
        public string DniPaciente { get; set; }

        // Médico
        public string NombreMedico { get; set; }

        // Especialidad
        public string Especialidad { get; set; }

        // Estado
        public int IdEstado { get; set; }
        public string Estado { get; set; }

        // Opcionales
        public string Observaciones { get; set; }
        public string Diagnostico { get; set; }
        public string FechaAlta { get; set; }
        public string UltimaModificacion { get; set; }
    }



}

