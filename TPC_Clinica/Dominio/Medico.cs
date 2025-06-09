using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Dominio
{
    public class Medico
    {
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public List<Especialidad> Especialidad { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        //public string Turno { get; set; } turno de trabajo
    }
}
