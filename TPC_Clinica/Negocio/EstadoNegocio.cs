using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class EstadoNegocio
    {
        public List<Estado> Listar()
        {
            List<Estado> lista = new List<Estado>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT idEstado as Id, descripcion FROM Estado");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Estado esp = new Estado();
                    esp.Id = (int)datos.Lector["Id"];
                    esp.Descripcion = (string)datos.Lector["descripcion"];
                    lista.Add(esp);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Estado> ListarActivos()
        {
            List<Estado> lista = new List<Estado>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT idEstado as Id, descripcion FROM Estado WHERE activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Estado esp = new Estado();
                    esp.Id = (int)datos.Lector["Id"];
                    esp.Descripcion = (string)datos.Lector["descripcion"];
                    lista.Add(esp);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Estado ListarConId(int id)
        {
            Estado lista = new Estado();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT idEstado as Id, descripcion FROM Estado WHERE idEstado = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Id = (int)datos.Lector["Id"];
                    lista.Descripcion = (string)datos.Lector["descripcion"];
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregarEstado(List<Estado> nuevo)
        {
            AccesoDatos datos;
            foreach (Estado item in nuevo)
            {
                datos = new AccesoDatos();
                try
                {
                    datos.setearConsulta("INSERT INTO Estado (descripcion) VALUES (@descripcion)");
                    datos.setearParametros("@descripcion", item.Descripcion);
                    datos.ejecutarAccion();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }
        }

        public void modificarEstado(Estado Estado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Estado set descripcion = @descripcion WHERE idEstado = @id");

                datos.setearParametros("@id", Estado.Id);
                datos.setearParametros("@descripcion", Estado.Descripcion);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminarEstado(Estado Estado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Estado set Activo = 0 WHERE idEstado = @id");

                datos.setearParametros("@id", Estado.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
