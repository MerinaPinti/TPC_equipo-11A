using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TipoUsuarioNegocio
    {
        public List<TipoUsuario> Listar()
        {
            List<TipoUsuario> lista = new List<TipoUsuario>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("SELECT idTipoUsuario as Id, descripcion FROM TIPOUSUARIO");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    TipoUsuario tipo = new TipoUsuario();
                    tipo.Id = (int)datos.Lector["Id"];
                    tipo.Descripcion = (string)datos.Lector["descripcion"];
                    lista.Add(tipo);
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

        public List<TipoUsuario> ListarActivos()
        {
            List<TipoUsuario> lista = new List<TipoUsuario>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("SELECT idTipoUsuario as Id, descripcion FROM TIPOUSUARIO WHERE activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    TipoUsuario tipo = new TipoUsuario();
                    tipo.Id = (int)datos.Lector["Id"];
                    tipo.Descripcion = (string)datos.Lector["descripcion"];
                    lista.Add(tipo);
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

        public TipoUsuario ListarConId(int id)
        {
            TipoUsuario tipo = new TipoUsuario();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("SELECT idTipoUsuario as Id, descripcion FROM TIPOUSUARIO WHERE idTipoUsuario = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    tipo.Id = (int)datos.Lector["Id"];
                    tipo.Descripcion = (string)datos.Lector["descripcion"];
                }

                return tipo;
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

        public void agregarTipoUsuario(List<TipoUsuario> nuevos)
        {
            AccesoDatos datos = null;
            try
            {
                foreach (TipoUsuario item in nuevos)
                {
                    datos = new AccesoDatos();

                    datos.setearConsulta("INSERT INTO TipoUsuario (descripcion, Activo) VALUES (@descripcion, 1)");
                    datos.setearParametros("@descripcion", item.Descripcion);
                    datos.ejecutarAccion();
                    datos.cerrarConexion();
                }
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

        public void modificarTipoUsuario(TipoUsuario tipo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("UPDATE TIPOUSUARIO set descripcion = @descripcion WHERE idTipoUsuario = @id");

                datos.setearParametros("@id", tipo.Id);
                datos.setearParametros("@descripcion", tipo.Descripcion);
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

        public void eliminarTipoUsuario(TipoUsuario tipo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("UPDATE TipoUsuario SET Activo = 0 WHERE idTipoUsuario = @id");

                datos.setearParametros("@id", tipo.Id);
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

        public bool enUso(int idTipoUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                Especialidad esp = new Especialidad();
                datos.setearConsulta("SELECT 1 FROM TipoUsuario TU  INNER JOIN Usuario U ON TU.idTipoUsuario = U.idTipoUsuario WHERE U.idTipoUsuario = @idTipoUsuario AND U.activo = 1");
                datos.setearParametros("@idTipoUsuario", idTipoUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    return true;
                }

                return false;
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