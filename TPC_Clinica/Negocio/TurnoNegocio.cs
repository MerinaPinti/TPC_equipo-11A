using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    internal class TurnoNegocio
    {
        public void agregarTurno(Turno nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"INSERT INTO Turno 
                (idPaciente, idMedico, fecha, hora, idEstado, fechaAlta, ultimaModificacion)
                VALUES 
                (@idPaciente, @idMedico, @fecha, @hora, @idEstado, GETDATE(), GETDATE())");

                datos.setearParametros("@idPaciente", nuevo.Paciente);
                datos.setearParametros("@idMedico", nuevo.Medico);
                datos.setearParametros("@fecha", nuevo.Fecha);
                datos.setearParametros("@hora", nuevo.Hora);
                datos.setearParametros("@idEstado", nuevo.Estado);

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
                P.nombre AS NombrePaciente,
                P.apellido AS ApellidoPaciente,
                M.nombre AS NombreMedico,
                M.apellido AS ApellidoMedico,
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
                    Turno t = new Turno();
                    t.NroTurno = datos.Lector["idTurno"].ToString();

                    t.Fecha = (DateTime)datos.Lector["fecha"];
                    t.Hora = (int)datos.Lector["hora"];
                    t.Observaciones = datos.Lector["observaciones"]?.ToString();
                    t.Diagnostico = datos.Lector["diagnostico"]?.ToString();

                    if (datos.Lector["fechaAlta"] != DBNull.Value)
                        t.FinTurno = (DateTime)datos.Lector["fechaAlta"];

                    if (datos.Lector["ultimaModificacion"] != DBNull.Value)
                        t.UltimaModificacion = (DateTime)datos.Lector["ultimaModificacion"];

                    t.Estado = datos.Lector["EstadoDescripcion"].ToString();

                    // PACIENTE
                    t.Paciente = new Paciente
                    {
                        Nombre = datos.Lector["NombrePaciente"].ToString(),
                        Apellido = datos.Lector["ApellidoPaciente"].ToString()
                    };

                    // MÉDICO
                    t.Medico = new Medico
                    {
                        Nombre = datos.Lector["NombreMedico"].ToString(),
                        Apellido = datos.Lector["ApellidoMedico"].ToString()
                    };

                    t.Activo = (bool)datos.Lector["activo"];

                    lista.Add(t);
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

    }
}
