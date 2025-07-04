using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class HorarioAtencionNegocio
    {
        public List<HorarioAtencion> listar()
        {
            List<HorarioAtencion> lista = new List<HorarioAtencion>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT * FROM HorarioAtencion WHERE activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    HorarioAtencion horario = new HorarioAtencion();
                    horario.IdHorarioAtencion = (int)datos.Lector["idHorarioAtencion"];
                    horario.IdMedico = (int)datos.Lector["idMedico"];
                    horario.IdEspecialidad = (int)datos.Lector["idEspecialidad"];
                    horario.Turno.IdTurnoTrabajo = (int)datos.Lector["idTurnoTrabajo"];
                    horario.DiaSemana = (byte)datos.Lector["diaSemana"];

                    lista.Add(horario);
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

        public void eliminarHorarioAtencion(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE HorarioAtencion SET activo = 0 WHERE idHorarioAtencion = @id");
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

        public void InsertarHorarioAtencion(int idMedico, int idEspecialidad, int idTurnoTrabajo, int diaSemana)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO HorarioAtencion (idMedico, idEspecialidad, idTurnoTrabajo, diaSemana) VALUES (@idMedico, @idEspecialidad, @idTurnoTrabajo, @diaSemana)");
                datos.setearParametros("@idMedico", idMedico);
                datos.setearParametros("@idEspecialidad", idEspecialidad);
                datos.setearParametros("@idTurnoTrabajo", idTurnoTrabajo);
                datos.setearParametros("@diaSemana", diaSemana);
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

        public List<HorarioAtencion> ListarPorMedicoYEspecialidad(int idMedico, int idEspecialidad)
        {
            List<HorarioAtencion> lista = new List<HorarioAtencion>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT H.diaSemana, T.horaInicio, T.horaFin
                               FROM HorarioAtencion H
                               INNER JOIN TurnoTrabajo T ON H.idTurnoTrabajo = T.idTurnoTrabajo
                               WHERE H.idMedico = @idMedico AND H.idEspecialidad = @idEspecialidad AND H.activo = 1");
                datos.setearParametros("@idMedico", idMedico);
                datos.setearParametros("@idEspecialidad", idEspecialidad);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    HorarioAtencion h = new HorarioAtencion();
                    h.DiaSemana = (byte)datos.Lector["diaSemana"];
                    h.Turno = new TurnoTrabajo
                    {
                        HoraInicio = (TimeSpan)datos.Lector["horaInicio"],
                        HoraFin = (TimeSpan)datos.Lector["horaFin"]
                    };
                    lista.Add(h);
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
    }
}
