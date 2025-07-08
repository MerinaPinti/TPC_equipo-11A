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
                datos.setearConsulta("SELECT HA.idHorarioAtencion, HA.idMedico, HA.idEspecialidad, HA.idTurnoTrabajo, HA.diaSemana, M.apellido, M.nombre, M.email, M.matricula, M.telefono, E.idEspecialidad, E.descripcion, TT.horaInicio, TT.horaFin FROM HorarioAtencion HA INNER JOIN Medico M ON HA.idMedico = M.idMedico INNER JOIN Especialidad E ON HA.idEspecialidad = E.idEspecialidad INNER JOIN TurnoTrabajo TT ON TT.idTurnoTrabajo = HA.idTurnoTrabajo WHERE HA.activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    HorarioAtencion horario = new HorarioAtencion();
                    horario.Id = (int)datos.Lector["idHorarioAtencion"];
                    horario.Medico = new Medico
                    {
                        IdMedico = (int)datos.Lector["idMedico"],
                        Apellido = (string)datos.Lector["apellido"],
                        Nombre = (string)datos.Lector["nombre"],
                        Email = (string)datos.Lector["email"],
                        Especialidad = new List<Especialidad>(),
                    };
                    horario.Especialidad = new Especialidad { Id = (int)datos.Lector["idEspecialidad"], Descripcion = (string)datos.Lector["descripcion"] };
                    horario.HorarioInicio = (TimeSpan)datos.Lector["horaInicio"];
                    horario.HorarioFin = (TimeSpan)datos.Lector["horaFin"];
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

        public HorarioAtencion listar(int id)
        {
            HorarioAtencion horario = new HorarioAtencion();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT HA.idHorarioAtencion, HA.idMedico, HA.idEspecialidad, HA.idTurnoTrabajo, HA.diaSemana, M.apellido, M.nombre, M.email, M.matricula, M.telefono, E.idEspecialidad, E.descripcion, TT.horaInicio, TT.horaFin FROM HorarioAtencion HA INNER JOIN Medico M ON HA.idMedico = M.idMedico INNER JOIN Especialidad E ON HA.idEspecialidad = E.idEspecialidad INNER JOIN TurnoTrabajo TT ON TT.idTurnoTrabajo = HA.idTurnoTrabajo WHERE HA.activo = 1 AND HA.idHorarioAtencion = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    horario.Id = (int)datos.Lector["idHorarioAtencion"];
                    horario.Medico = new Medico
                    {
                        IdMedico = (int)datos.Lector["idMedico"],
                        Apellido = (string)datos.Lector["apellido"],
                        Nombre = (string)datos.Lector["nombre"],
                        Email = (string)datos.Lector["email"],
                        Especialidad = new List<Especialidad>(),
                    };
                    horario.Especialidad = new Especialidad { Id = (int)datos.Lector["idEspecialidad"], Descripcion = (string)datos.Lector["descripcion"] };
                    horario.HorarioInicio = (TimeSpan)datos.Lector["horaInicio"];
                    horario.HorarioFin = (TimeSpan)datos.Lector["horaFin"];
                    horario.DiaSemana = (byte)datos.Lector["diaSemana"];
                    return horario;
                }
                return null;

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

        public List<HorarioAtencion> listarConIdMedico(int id)
        {
            List<HorarioAtencion> horarios = new List<HorarioAtencion>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT HA.idHorarioAtencion, HA.idMedico, HA.idEspecialidad, HA.idTurnoTrabajo, HA.diaSemana, M.apellido, M.nombre, M.email, M.matricula, M.telefono, E.idEspecialidad, E.descripcion, TT.horaInicio, TT.horaFin FROM HorarioAtencion HA INNER JOIN Medico M ON HA.idMedico = M.idMedico INNER JOIN Especialidad E ON HA.idEspecialidad = E.idEspecialidad INNER JOIN TurnoTrabajo TT ON TT.idTurnoTrabajo = HA.idTurnoTrabajo WHERE HA.activo = 1 AND HA.idMedico = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    HorarioAtencion horario = new HorarioAtencion();
                    horario.Id = (int)datos.Lector["idHorarioAtencion"];
                    horario.Medico = new Medico
                    {
                        IdMedico = (int)datos.Lector["idMedico"],
                        Apellido = (string)datos.Lector["apellido"],
                        Nombre = (string)datos.Lector["nombre"],
                        Email = (string)datos.Lector["email"],
                        Especialidad = new List<Especialidad>(),
                    };
                    horario.Especialidad = new Especialidad { Id = (int)datos.Lector["idEspecialidad"], Descripcion = (string)datos.Lector["descripcion"] };
                    horario.HorarioInicio = (TimeSpan)datos.Lector["horaInicio"];
                    horario.HorarioFin = (TimeSpan)datos.Lector["horaFin"];
                    horario.DiaSemana = (byte)datos.Lector["diaSemana"];
                    horarios.Add(horario);
                }
                return horarios;

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

        public void modificarHorario(HorarioAtencion horario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE HorarioAtencion SET idMedico = @idMedico, idEspecialidad = @idEspecialidad, idTurnoTrabajo = @idTurnoTrabajo, diaSemana = @diaSemana WHERE idHorarioAtencion = @id");
                datos.setearParametros("@id", horario.Id);
                datos.setearParametros("@idEspecialidad", horario.Especialidad.Id);
                datos.setearParametros("@idMedico", horario.Medico.IdMedico);
                datos.setearParametros("@idTurnoTrabajo", horario.Turno.IdTurnoTrabajo);
                datos.setearParametros("@diaSemana", horario.DiaSemana);
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

        public void eliminarLogico(int id)
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

        public bool existeHorario(TimeSpan horarioInicio, TimeSpan horarioFin, Medico medico, int diaSemana)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                SELECT 1
                FROM HorarioAtencion HA
                INNER JOIN TurnoTrabajo TT ON HA.idTurnoTrabajo = TT.idTurnoTrabajo
                WHERE (TT.horaInicio < @horarioFin AND TT.horaFin > @horarioInicio)
                AND idMedico = @idMedico
                AND diaSemana = @diaSemana
                AND HA.activo = 1");

                datos.setearParametros("@idMedico", medico.IdMedico);
                datos.setearParametros("@horarioInicio", horarioInicio);
                datos.setearParametros("@diaSemana", diaSemana);
                datos.setearParametros("@horarioFin", horarioFin);

                datos.ejecutarLectura();

                return datos.Lector.Read();
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

        public bool existeHorarioDistinto(TimeSpan horarioInicio, TimeSpan horarioFin, Medico medico, int diaSemana, int idHorarioAtencion)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                SELECT 1
                FROM HorarioAtencion HA
                INNER JOIN TurnoTrabajo TT ON HA.idTurnoTrabajo = TT.idTurnoTrabajo
                WHERE (TT.horaInicio < @horarioFin AND TT.horaFin > @horarioInicio)
                AND idMedico = @idMedico
                AND diaSemana = @diaSemana
                AND HA.activo = 1
                AND HA.idHorarioAtencion != @idHorarioAtencion");

                datos.setearParametros("@idMedico", medico.IdMedico);
                datos.setearParametros("@horarioInicio", horarioInicio);
                datos.setearParametros("@diaSemana", diaSemana);
                datos.setearParametros("@horarioFin", horarioFin);
                datos.setearParametros("@idHorarioAtencion", idHorarioAtencion);

                datos.ejecutarLectura();

                return datos.Lector.Read();
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
