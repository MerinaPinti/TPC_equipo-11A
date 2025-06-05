using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Turno
    {
        public string NrodeTurno { get; set; }
        public Paciente DNIpaciente { get; set; }
        public Medico Medico { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }
        public string Diagnostico { get; set; }
        public DateTime FinTurno { get; set; }
        public DateTime UltimaModificacion {  get; set; }

}
}
