using Dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EspecialidadNegocio
    {
        public List<Especialidad> Listar()
        {
            List<Especialidad> lista = new List<Especialidad>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT idEspecialidad as Id, descripcion FROM ESPECIALIDAD");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Especialidad esp = new Especialidad();
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

        public List<Especialidad> ListarActivos()
        {
            List<Especialidad> lista = new List<Especialidad>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT idEspecialidad as Id, descripcion FROM ESPECIALIDAD WHERE activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Especialidad esp = new Especialidad();
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

        public Especialidad ListarConId(int id)
        {
            Especialidad lista = new Especialidad();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT idEspecialidad as Id, descripcion FROM ESPECIALIDAD WHERE idEspecialidad = @id");
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

        public void agregarEspecialidad(List<Especialidad> nuevo)
        {
            AccesoDatos datos;
            foreach (Especialidad item in nuevo)
            {
                datos = new AccesoDatos();
                try
                {
                    datos.setearConsulta("INSERT INTO Especialidad (descripcion) VALUES (@descripcion)");
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

        public void modificarEspecialidad(Especialidad especialidad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Especialidad set descripcion = @descripcion WHERE idEspecialidad = @id");

                datos.setearParametros("@id", especialidad.Id);
                datos.setearParametros("@descripcion", especialidad.Descripcion);
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
        public void eliminarEspecialidad(Especialidad especialidad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Especialidad set Activo = 0 WHERE idEspecialidad = @id");

                datos.setearParametros("@id", especialidad.Id);
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

        public List<Especialidad> ListarPorMedico(int idMedico)
        {
            List<Especialidad> lista = new List<Especialidad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT E.idEspecialidad, E.Descripcion 
                               FROM Especialidad E
                               INNER JOIN Especialidades_Medicos EM ON EM.IDESPECIALIDAD = E.idEspecialidad
                               WHERE EM.IDMEDICO = @idMedico AND EM.Activo = 1");

                datos.setearParametros("@idMedico", idMedico);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Especialidad esp = new Especialidad();
                    esp.Id = (int)datos.Lector["idEspecialidad"];
                    esp.Descripcion = datos.Lector["Descripcion"].ToString();
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

        public bool enUso(int idEspecialidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                Especialidad esp = new Especialidad();
                datos.setearConsulta("SELECT 1 FROM Especialidad E INNER JOIN Turno T ON E.idEspecialidad = T.idEspecialidad INNER JOIN HorarioAtencion HA ON E.idEspecialidad = HA.idEspecialidad INNER JOIN Especialidades_Medicos EM ON E.idEspecialidad = EM.IDESPECIALIDAD INNER JOIN Medico M ON EM.IDMEDICO = M.idMedico WHERE E.activo = 1 AND T.activo = 1 AND HA.activo = 1 AND EM.Activo = 1 AND E.idEspecialidad = @idEspecialidad");
                datos.setearParametros("@idEspecialidad", idEspecialidad);
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

