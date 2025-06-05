using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Usuario
    {
        public int Id { get; set; }
        public TipoUsuario IdTipo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
