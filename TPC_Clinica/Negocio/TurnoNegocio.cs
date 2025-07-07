using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TurnoNegocio
    {
        public void agregarTurno(Turno nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
            INSERT INTO Turno 
            (idPaciente, idMedico, fecha, hora, idEstado, fechaAlta, ultimaModificacion, activo)
            VALUES 
            (@idPaciente, @idMedico, @fecha, @hora, @idEstado, GETDATE(), GETDATE(), 1)");

                datos.setearParametros("@idPaciente", nuevo.Paciente.IdPaciente);
                datos.setearParametros("@idMedico", nuevo.Medico.IdMedico);
                datos.setearParametros("@fecha", nuevo.Fecha);
                datos.setearParametros("@hora", nuevo.Hora);
                datos.setearParametros("@idEstado", 1); // Asignado

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

        public void modificarTurno(Turno turno)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"UPDATE Turno SET 
                    idPaciente = @idPaciente,
                    idMedico = @idMedico,
                    fecha = @fecha,
                    hora = @hora,
                    idEstado = @idEstado,                    
                    ultimaModificacion = GETDATE()
                    WHERE idTurno = @idTurno");

                datos.setearParametros("@idTurno", int.Parse(turno.NroTurno));
                datos.setearParametros("@idPaciente", turno.Paciente);
                datos.setearParametros("@idMedico", turno.Medico);
                datos.setearParametros("@fecha", turno.Fecha);
                datos.setearParametros("@hora", turno.Hora);
                datos.setearParametros("@idEstado", turno.Estado);

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

        public List<Turno> ListarTurnos()
        {
            List<Turno> lista = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT 
                T.idTurno,
                T.fecha,
                T.hora,
                T.observaciones,
                T.diagnostico,
                T.fechaAlta,
                T.ultimaModificacion,
                T.activo,
                P.idPaciente,
                P.nombre AS NombrePaciente,
                P.apellido AS ApellidoPaciente,
                M.idMedico,
                M.nombre AS NombreMedico,
                M.apellido AS ApellidoMedico,
                E.idEstado,
                E.descripcion AS EstadoDescripcion
            FROM Turno T
            INNER JOIN Paciente P ON T.idPaciente = P.idPaciente
            INNER JOIN Medico M ON T.idMedico = M.idMedico
            INNER JOIN Estado E ON T.idEstado = E.idEstado
            WHERE T.activo = 1
        ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Turno turno = new Turno();
                    turno.NroTurno = datos.Lector["idTurno"].ToString();
                    turno.Fecha = (DateTime)datos.Lector["fecha"];
                    turno.Hora = (TimeSpan)datos.Lector["hora"];
                    turno.Observaciones = datos.Lector["observaciones"]?.ToString();
                    turno.Diagnostico = datos.Lector["diagnostico"]?.ToString();

                    if (datos.Lector["fechaAlta"] != DBNull.Value)
                        turno.FinTurno = (DateTime)datos.Lector["fechaAlta"];

                    if (datos.Lector["ultimaModificacion"] != DBNull.Value)
                        turno.UltimaModificacion = (DateTime)datos.Lector["ultimaModificacion"];


                    turno.Estado = new Estado
                    {
                        Id = (int)datos.Lector["idEstado"],
                        Descripcion = datos.Lector["EstadoDescripcion"].ToString()
                    };


                    turno.Paciente = new Paciente
                    {
                        IdPaciente = (int)datos.Lector["idPaciente"],
                        Nombre = datos.Lector["NombrePaciente"].ToString(),
                        Apellido = datos.Lector["ApellidoPaciente"].ToString()
                    };


                    turno.Medico = new Medico
                    {
                        IdMedico = (int)datos.Lector["idMedico"],
                        Nombre = datos.Lector["NombreMedico"].ToString(),
                        Apellido = datos.Lector["ApellidoMedico"].ToString()
                    };

                    turno.Activo = (bool)datos.Lector["activo"];

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
        public void eliminarLogicoTurno(int idTurno)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"UPDATE Turno 
                               SET activo = 0, ultimaModificacion = GETDATE() 
                               WHERE idTurno = @idTurno");

                datos.setearParametros("@idTurno", idTurno);
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


        public void cerrarTurno(int idTurno, string observaciones, string diagnostico)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            UPDATE Turno SET 
                observaciones = @obs,
                diagnostico = @diag,
                idEstado = 5, -- Cerrado
                ultimaModificacion = GETDATE()
            WHERE idTurno = @idTurno");

                datos.setearParametros("@idTurno", idTurno);
                datos.setearParametros("@obs", observaciones);
                datos.setearParametros("@diag", diagnostico);

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

        // Listar turnos asignados para el calendario
        public List<Turno> ListarTurnosAsignados(int idMedico)
        {
            List<Turno> lista = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
    SELECT 
        T.idTurno, T.fecha, T.hora, T.idEstado,
        P.IdPaciente, P.Nombre AS NombrePaciente, P.Apellido AS ApellidoPaciente,
        M.IdMedico, M.Nombre AS NombreMedico, M.Apellido AS ApellidoMedico
    FROM Turno T
    INNER JOIN Paciente P ON P.IdPaciente = T.idPaciente
    INNER JOIN Medico M ON M.IdMedico = T.idMedico
    WHERE T.idMedico = @idMedico AND T.activo = 1
");

                datos.setearParametros("@idMedico", idMedico);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Turno turno = new Turno();
                    turno.NroTurno = datos.Lector["idTurno"].ToString();
                    turno.Fecha = (DateTime)datos.Lector["fecha"];
                    turno.Hora = (TimeSpan)datos.Lector["hora"];

                    // Carga Estado del turno
                    turno.Estado = new Estado
                    {
                        Id = Convert.ToInt32(datos.Lector["idEstado"])
                    };

                    // Carga Paciente
                    turno.Paciente = new Paciente
                    {
                        IdPaciente = Convert.ToInt32(datos.Lector["IdPaciente"]),
                        Nombre = datos.Lector["NombrePaciente"].ToString(),
                        Apellido = datos.Lector["ApellidoPaciente"].ToString()
                    };

                    // Carga Médico
                    turno.Medico = new Medico
                    {
                        IdMedico = Convert.ToInt32(datos.Lector["IdMedico"]),
                        Nombre = datos.Lector["NombreMedico"].ToString(),
                        Apellido = datos.Lector["ApellidoMedico"].ToString()
                    };

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


        public void actualizarTurnoRecep(Turno turno)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Turno SET idEstado = @idEstado, ultimaModificacion = GETDATE() WHERE idTurno = @NroTurno");
                datos.setearParametros("@NroTurno", turno.NroTurno);
                datos.setearParametros("@idEstado", turno.Estado.Id);
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
