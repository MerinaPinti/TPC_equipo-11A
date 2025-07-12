using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            (idPaciente, idMedico, fecha, hora, idEstado, fechaAlta, ultimaModificacion, activo, idEspecialidad)
            VALUES 
            (@idPaciente, @idMedico, @fecha, @hora, @idEstado, GETDATE(), GETDATE(), 1, @idEspecialidad)");

                datos.setearParametros("@idPaciente", nuevo.Paciente.IdPaciente);
                datos.setearParametros("idEspecialidad", nuevo.Especialidad.Id);
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
                        ES.descripcion AS Especialidad,
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
                    INNER JOIN Especialidad ES ON T.idEspecialidad = ES.idEspecialidad
                    WHERE T.activo = 1"
                );

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

        public List<Turno> ListarTurnos(string dni)
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
                        ES.descripcion AS Especialidad,
                        T.activo,
                        P.idPaciente,
                        P.nombre AS NombrePaciente,
                        P.apellido AS ApellidoPaciente,
                        M.idMedico,
                        M.nombre AS NombreMedico,
                        M.apellido AS ApellidoMedico,
                        E.idEstado,
                        E.descripcion AS EstadoDescripcion,
                        ES.idEspecialidad,
                        ES.descripcion AS Especilidad
                    FROM Turno T
                    INNER JOIN Paciente P ON T.idPaciente = P.idPaciente
                    INNER JOIN Medico M ON T.idMedico = M.idMedico
                    INNER JOIN Estado E ON T.idEstado = E.idEstado
                    INNER JOIN Especialidad ES ON T.idEspecialidad = ES.idEspecialidad
                    WHERE T.activo = 1
                    AND T.idEstado = 5
                    AND P.DNI = @dni
                    ORDER BY fecha"
                );
                datos.setearParametros("@dni", dni);
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

                    turno.Especialidad = new Especialidad
                    {
                        Descripcion = (string)datos.Lector["Especialidad"],
                        Id = (int)datos.Lector["idEspecialidad"]
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
                    WHERE idTurno = @idTurno"
                );

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
        public List<TurnoVista> ListarTurnosAsignadosSemana(int idMedico, int idEspecialidad)
        {
            List<TurnoVista> lista = new List<TurnoVista>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT t.idTurno, t.fecha, t.hora, t.idEstado, p.nombre, p.apellido
                    FROM Turno t
                    INNER JOIN Paciente p ON p.idPaciente = t.idPaciente
                    WHERE t.idMedico = @idMedico
                      AND t.idEspecialidad = @idEspecialidad
                      AND t.fecha >= CAST(GETDATE() AS DATE)
                      AND t.activo = 1
                    ORDER BY t.fecha ASC, t.hora ASC"
                );

                datos.setearParametros("@idMedico", idMedico);
                datos.setearParametros("@idEspecialidad", idEspecialidad);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    TurnoVista turno = new TurnoVista
                    {
                        NroTurno = datos.Lector["idTurno"].ToString(),
                        Fecha = ((DateTime)datos.Lector["fecha"]).ToShortDateString(),
                        Hora = datos.Lector["hora"].ToString().Substring(0, 5),
                        NombrePaciente = datos.Lector["nombre"].ToString() + " " + datos.Lector["apellido"].ToString(),
                        IdEstado = (int)datos.Lector["idEstado"]
                    };

                    lista.Add(turno);
                }

                return lista;
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
                datos.setearConsulta("UPDATE Turno SET idEstado = @idEstado, ultimaModificacion = GETDATE() WHERE idTurno = @idTurno");
                datos.setearParametros("@idTurno", Convert.ToInt32(turno.NroTurno));
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


        

        public void actualizarTurnoMedico(Turno turno)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    UPDATE Turno 
                    SET 
                        idEstado = @idEstado,
                        observaciones = @observaciones,
                        diagnostico = @diagnostico,
                        ultimaModificacion = GETDATE()
                    WHERE idTurno = @NroTurno"
                );

                datos.setearParametros("@NroTurno", turno.NroTurno);
                datos.setearParametros("@idEstado", turno.Estado.Id);

                // Si alguno de los campos puede ser nulo:
                datos.setearParametros("@observaciones", turno.Observaciones ?? (object)DBNull.Value);
                datos.setearParametros("@diagnostico", turno.Diagnostico ?? (object)DBNull.Value);

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
                    WHERE T.idMedico = @idMedico AND T.activo = 1"
                );

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

        public Turno ObtenerPorId(string nroTurno)
        {
            Turno turno = new Turno();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT t.idTurno, t.fecha, t.hora, t.observaciones, t.diagnostico, t.idEspecialidad, 
                           p.nombre AS nombrePaciente, 
                           p.apellido AS apellidoPaciente, 
                           p.email AS emailPaciente,
                           m.idMedico, m.nombre AS nombreMedico, 
                           m.apellido AS apellidoMedico
                    FROM Turno t
                    INNER JOIN Paciente p ON t.idPaciente = p.idPaciente
                    INNER JOIN Medico m ON t.idMedico = m.idMedico
                    WHERE t.idTurno = @idTurno
                ");

                datos.setearParametros("@idTurno", nroTurno);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    turno.NroTurno = nroTurno;
                    turno.Fecha = (DateTime)datos.Lector["fecha"];
                    turno.Hora = (TimeSpan)datos.Lector["hora"];
                    turno.Observaciones = datos.Lector["observaciones"]?.ToString();
                    turno.Diagnostico = datos.Lector["diagnostico"]?.ToString();
                    
                    turno.Paciente = new Paciente
                    {
                        Nombre = datos.Lector["nombrePaciente"].ToString(),
                        Apellido = datos.Lector["apellidoPaciente"].ToString(),
                        Email = datos.Lector["emailPaciente"].ToString()

                    };

                    
                    turno.Especialidad = new Especialidad
                    {
                        Id = Convert.ToInt32(datos.Lector["idEspecialidad"])
                    };

                    turno.Medico = new Medico
                    {
                        Nombre = datos.Lector["nombreMedico"].ToString(),
                        Apellido = datos.Lector["apellidoMedico"].ToString()
                    };
                }

                return turno;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Turno> ListarTurnosPorDNI(string dni, int idEstado)
        {
            List<Turno> lista = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT 
                        t.idTurno,
                        t.fecha,
                        t.hora,
                        m.Nombre + ' ' + m.Apellido AS Medico,
                        e.Descripcion AS Especialidad,
                        est.Descripcion AS Estado,
                        p.Nombre AS NombrePaciente,
                        p.Apellido AS ApellidoPaciente,
                        p.Email AS EmailPaciente,
                        DNI=dni
                    FROM Turno t
                    INNER JOIN Paciente p ON p.idPaciente = t.idPaciente
                    INNER JOIN Medico m ON m.idMedico = t.idMedico
                    LEFT JOIN ESPECIALIDADES_MEDICOS em ON em.idMedico = m.idMedico
                    LEFT JOIN Especialidad e ON e.idEspecialidad = em.idEspecialidad
                    INNER JOIN Estado est ON est.idEstado = t.idEstado
                    WHERE p.DNI = @DNI
                        AND (@idEstado = 0 OR t.idEstado = @idEstado)
                    ORDER BY t.fecha ASC, t.hora ASC"
                );

                datos.setearParametros("@DNI", dni);
                datos.setearParametros("@idEstado", idEstado);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Turno t = new Turno();
                    t.NroTurno = datos.Lector["idTurno"].ToString();
                    t.Fecha = (DateTime)datos.Lector["fecha"];
                    t.Hora = (TimeSpan)datos.Lector["hora"];

                    t.Medico = new Medico
                    {
                        Nombre = datos.Lector["Medico"].ToString()
                    };

                    t.Medico.Especialidad = new List<Especialidad>
                        {
                            new Especialidad
                            {
                                Descripcion = datos.Lector["Especialidad"].ToString()
                            }
                        };

                    t.Estado = new Estado
                    {
                        Descripcion = datos.Lector["Estado"].ToString()
                    };
                    t.Paciente = new Paciente
                    {
                        Nombre = datos.Lector["NombrePaciente"].ToString(),
                        Apellido = datos.Lector["ApellidoPaciente"].ToString(),
                        Email = datos.Lector["EmailPaciente"].ToString(),
                        DNI = dni
                    };


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

        public List<Turno> ListarTurnoPorDNI(string dni, int idMedico, int idEspecialidad)
        {
            List<Turno> lista = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT 
                    t.idTurno,
                    t.fecha,
                    t.hora,
                    t.Observaciones,
                    t.Diagnostico,
                    m.Nombre + ' ' + m.Apellido AS Medico,
                    e.idEspecialidad,
                    e.Descripcion AS Especialidad,
                    est.Descripcion AS Estado,
                    p.Nombre AS NombrePaciente,
                    p.Apellido AS ApellidoPaciente,
                    p.Email AS EmailPaciente,
                    DNI = dni
                FROM Turno t
                INNER JOIN Paciente p ON p.idPaciente = t.idPaciente
                INNER JOIN Medico m ON m.idMedico = t.idMedico
                INNER JOIN Especialidad e ON e.idEspecialidad = t.idEspecialidad
                INNER JOIN Estado est ON est.idEstado = t.idEstado
                WHERE p.DNI = @DNI
                    AND (t.idEstado = 5)
                    AND (@idMedico = 0 OR m.idMedico = @idMedico)
                    AND (@idEspecialidad = 0 OR e.idEspecialidad = @idEspecialidad)
                ORDER BY t.fecha DESC");

                datos.setearParametros("@DNI", dni);
                datos.setearParametros("@idMedico", idMedico);
                datos.setearParametros("@idEspecialidad", idEspecialidad);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Turno t = new Turno();
                    t.NroTurno = datos.Lector["idTurno"].ToString();
                    t.Fecha = (DateTime)datos.Lector["fecha"];
                    t.Hora = (TimeSpan)datos.Lector["hora"];
                    t.Observaciones = datos.Lector["Observaciones"] != DBNull.Value ? datos.Lector["Observaciones"].ToString() : "";
                    t.Diagnostico = datos.Lector["Diagnostico"] != DBNull.Value ? datos.Lector["Diagnostico"].ToString() : "";



                    t.Medico = new Medico
                    {
                        Nombre = datos.Lector["Medico"].ToString()
                    };

                    t.Especialidad = new Especialidad
                    {
                        Id = (int)datos.Lector["idEspecialidad"],
                        Descripcion = datos.Lector["Especialidad"].ToString()
                    };

                    t.Estado = new Estado
                    {
                        Descripcion = datos.Lector["Estado"].ToString()
                    };
                    t.Paciente = new Paciente
                    {
                        Nombre = datos.Lector["NombrePaciente"].ToString(),
                        Apellido = datos.Lector["ApellidoPaciente"].ToString(),
                        Email = datos.Lector["EmailPaciente"].ToString(),
                        DNI = dni
                    };


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

        public List<Turno> ListarTurnosDelDia(string dniPaciente, string nombreMedico, int idEspecialidad)
        {
            List<Turno> lista = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"
            SELECT T.idTurno, T.fecha, T.hora, 
       P.nombre AS NombrePaciente, P.apellido AS ApellidoPaciente, P.DNI,
       M.nombre AS NombreMedico, M.apellido AS ApellidoMedico,
       E.descripcion AS Especialidad,
       EST.descripcion AS Estado,
       T.idEstado
    FROM Turno T
    INNER JOIN Paciente P ON T.idPaciente = P.idPaciente
    INNER JOIN Medico M ON T.idMedico = M.idMedico
    INNER JOIN Especialidad E ON T.idEspecialidad = E.idEspecialidad
    INNER JOIN Estado EST ON T.idEstado = EST.idEstado
    WHERE T.fecha = CAST(GETDATE() AS DATE)
      AND T.activo = 1";


                // Agregar filtros 
                if (!string.IsNullOrEmpty(dniPaciente))
                    consulta += " AND P.DNI LIKE @dni ";

                if (!string.IsNullOrEmpty(nombreMedico))
                    consulta += " AND (M.nombre + ' ' + M.apellido) LIKE @nombreMedico ";

                if (idEspecialidad != 0)
                    consulta += " AND E.idEspecialidad = @idEspecialidad ";

                // Ordena Acendente 
                consulta += " ORDER BY T.fecha ASC, T.hora ASC";

                // Ejecutar consulta
                datos.setearConsulta(consulta);

                if (!string.IsNullOrEmpty(dniPaciente))
                    datos.setearParametros("@dni", "%" + dniPaciente + "%");

                if (!string.IsNullOrEmpty(nombreMedico))
                    datos.setearParametros("@nombreMedico", "%" + nombreMedico + "%");

                if (idEspecialidad != 0)
                    datos.setearParametros("@idEspecialidad", idEspecialidad);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Turno turno = new Turno
                    {
                        NroTurno = datos.Lector["idTurno"].ToString(),
                        Fecha = (DateTime)datos.Lector["fecha"],
                        Hora = (TimeSpan)datos.Lector["hora"],
                        Estado = new Estado
                        {
                            Id = (int)datos.Lector["idEstado"],
                            Descripcion = datos.Lector["Estado"].ToString()
                        },
                        Paciente = new Paciente
                        {
                            Nombre = datos.Lector["NombrePaciente"].ToString(),
                            Apellido = datos.Lector["ApellidoPaciente"].ToString(),
                            DNI = datos.Lector["DNI"].ToString()
                        },
                        Medico = new Medico
                        {
                            Nombre = datos.Lector["NombreMedico"].ToString(),
                            Apellido = datos.Lector["ApellidoMedico"].ToString()
                        },
                        Especialidad = new Especialidad
                        {
                            Descripcion = datos.Lector["Especialidad"].ToString()
                        }
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


        public List<Turno> ListarTurnosDelDiaMedico(int idMedico)
        {
            List<Turno> lista = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"
            SELECT T.idTurno, T.fecha, T.hora, 
                   P.nombre AS NombrePaciente, P.apellido AS ApellidoPaciente, P.DNI,
                   E.descripcion AS Especialidad,
                   EST.descripcion AS Estado,
                   T.idEstado
            FROM Turno T
            INNER JOIN Paciente P ON T.idPaciente = P.idPaciente
            INNER JOIN Especialidad E ON T.idEspecialidad = E.idEspecialidad
            INNER JOIN Estado EST ON T.idEstado = EST.idEstado
            WHERE T.fecha = CAST(GETDATE() AS DATE)
              AND T.idMedico = @idMedico
              AND T.idEstado = 6
              AND T.activo = 1
            ORDER BY T.hora ASC";

                datos.setearConsulta(consulta);
                datos.setearParametros("@idMedico", idMedico);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Turno turno = new Turno
                    {
                        NroTurno = datos.Lector["idTurno"].ToString(),
                        Fecha = (DateTime)datos.Lector["fecha"],
                        Hora = (TimeSpan)datos.Lector["hora"],
                        Estado = new Estado
                        {
                            Id = (int)datos.Lector["idEstado"],
                            Descripcion = datos.Lector["Estado"].ToString()
                        },
                        Paciente = new Paciente
                        {
                            Nombre = datos.Lector["NombrePaciente"].ToString(),
                            Apellido = datos.Lector["ApellidoPaciente"].ToString(),
                            DNI = datos.Lector["DNI"].ToString()
                        },
                        Especialidad = new Especialidad
                        {
                            Descripcion = datos.Lector["Especialidad"].ToString()
                        }
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


        

    }
}
