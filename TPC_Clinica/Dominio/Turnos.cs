using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Turnos
    {
        public string NrodeTurno { get; set; }
        public Pacientes DNIpaciente { get; set; }
        public Medicos Medico { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }
        public string Diagnostico { get; set; }
        public DateTime FinTurno { get; set; }
        public DateTime UltimaModificacion {  get; set; }

}
}
