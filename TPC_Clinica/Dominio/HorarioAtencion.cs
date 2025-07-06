using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class HorarioAtencion
    {
        public int Id { get; set; }
        public Medico Medico { get; set; }
        public Especialidad Especialidad { get; set; }
        public TimeSpan HorarioInicio { get; set; }
        public TimeSpan HorarioFin { get; set; }
        public int DiaSemana { get; set; }
        public TurnoTrabajo Turno { get; set; }
    }
}
