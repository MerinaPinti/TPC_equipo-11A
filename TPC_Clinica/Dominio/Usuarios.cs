using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Usuarios
    {
        public int Id { get; set; }
        public TiposUsuarios IdTipo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
