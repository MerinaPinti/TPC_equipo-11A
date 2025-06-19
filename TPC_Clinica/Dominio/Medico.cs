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
        public int IdMedico { get; set; }
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public List<Especialidad> Especialidad { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        //public string Turno { get; set; } turno de trabajo
        public bool Activo { get; set; }


        //Para mostrar nombre en el listado
        public string NombreCompleto
        {
            get { return $"{Nombre} {Apellido}"; }
        }

        //Para mostrar una lista string de especialidades en el listado
        public string EspecialidadesTexto
        {
            get
            {
                if (Especialidad == null || Especialidad.Count == 0)
                    return "-";
                return string.Join(", ", Especialidad.Select(e => e.Descripcion));
            }
        }
    }
}
