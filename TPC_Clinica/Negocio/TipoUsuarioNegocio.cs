using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Negocio;

namespace Negocio
{
    public class TipoUsuarioNegocio
    {
        public void agregarTipoUsuario(TipoUsuario tipo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO TipoUsuario (Descripcion) VALUES (@descripcion)");
                datos.setearParametros("@descripcion", tipo.Descripcion);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificarTipoUsuario(TipoUsuario tipo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE TipoUsuario SET Descripcion = @descripcion WHERE Id = @id");
                datos.setearParametros("@descripcion", tipo.Descripcion);
                datos.setearParametros("@id", tipo.Id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public TipoUsuario ListarConId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM TipoUsuario WHERE Id = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return new TipoUsuario
                    {
                        Id = (int)datos.Lector["Id"],
                        Descripcion = (string)datos.Lector["Descripcion"]
                    };
                }

                return null;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
