using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class TurnoTrabajoNegocio
    {
        public List<TurnoTrabajo> listar()
        {
            List<TurnoTrabajo> lista = new List<TurnoTrabajo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT * FROM TurnoTrabajo WHERE activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    TurnoTrabajo turno = new TurnoTrabajo();
                    turno.IdTurnoTrabajo = (int)datos.Lector["idTurnoTrabajo"];
                    turno.Descripcion = datos.Lector["descripcion"].ToString();
                    turno.HoraInicio = TimeSpan.Parse(datos.Lector["horaInicio"].ToString());
                    turno.HoraFin = TimeSpan.Parse(datos.Lector["horaFin"].ToString());

                    lista.Add(turno);
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

        public void eliminarTurnoTrabajo(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE TurnoTrabajo SET activo = 0 WHERE idTurnoTrabajo = @id");
                datos.setearParametros("@id", id);
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


        public int ObtenerIdTurnoTrabajo(string horaInicio, string horaFin)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT idTurnoTrabajo FROM TurnoTrabajo WHERE horaInicio = @horaInicio AND horaFin = @horaFin AND activo = 1");
                datos.setearParametros("@horaInicio", horaInicio);
                datos.setearParametros("@horaFin", horaFin);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["idTurnoTrabajo"];
                }
                return 0;
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

        public int InsertarTurnoTrabajo(string descripcion, TimeSpan horaInicio, TimeSpan horaFin)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO TurnoTrabajo (descripcion, horaInicio, horaFin) OUTPUT INSERTED.idTurnoTrabajo VALUES (@descripcion, @horaInicio, @horaFin)");
                datos.setearParametros("@descripcion", descripcion);
                datos.setearParametros("@horaInicio", horaInicio);
                datos.setearParametros("@horaFin", horaFin);

                return (int)datos.ejecutarScalar();
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
