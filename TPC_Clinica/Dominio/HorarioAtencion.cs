using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class HorarioAtencion
    {
        public int IdHorarioAtencion { get; set; }
        public int IdMedico { get; set; }
        public int IdEspecialidad { get; set; }
        public int IdTurnoTrabajo { get; set; }
        public int DiaSemana { get; set; } 
        public bool Activo { get; set; }
    }
}
