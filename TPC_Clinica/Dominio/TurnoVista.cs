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
            public string NombrePaciente { get; set; }

            public string Especialidad { get; set; }

           // Comentado hasta que se agregue a la base de datos
           // public string MotivoConsulta { get; set; }
            public int IdEstado { get; set; }
        }


        
        
 }

