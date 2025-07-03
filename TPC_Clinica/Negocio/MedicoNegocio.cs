using Dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Negocio
{
    public class MedicoNegocio
    {



        public List<Medico> listar() //1. Metodo para que lea los registros de la base de datos
        {
            List<Medico> lista = new List<Medico>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("SELECT M.idMedico, M.email, M.telefono, M.nombre, M.apellido, M.matricula, EM.IDMEDICO, EM.IDESPECIALIDAD, E.descripcion FROM MEDICO as M LEFT JOIN ESPECIALIDADES_MEDICOS AS EM ON EM.IDMEDICO = M.idMedico LEFT JOIN ESPECIALIDAD AS E ON E.idEspecialidad = EM.idEspecialidad WHERE M.activo = 1;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    int idMedico = (int)datos.Lector["idMedico"];
                    Medico existente = lista.FirstOrDefault(a => a.IdMedico == idMedico); //devuelve el primero que encuentra, si no encuentra devuelve null 

                    if (existente == null)
                    {
                        Medico aux = new Medico();
                        aux.IdMedico = idMedico;
                        aux.Matricula = (string)datos.Lector["matricula"];
                        aux.Nombre = (string)datos.Lector["nombre"];
                        aux.Apellido = (string)datos.Lector["apellido"];
                        aux.Email = (string)datos.Lector["email"];
                        aux.Telefono = (string)datos.Lector["telefono"];


                        //aux.Imagen = new Imagen();
                        //aux.Imagen.Url = datos.Lector["ImagenUrl"] != DBNull.Value ? datos.Lector["ImagenUrl"].ToString() : "";
                        aux.Especialidad = new List<Especialidad>();
                        //ID ESPECIALIDAD DE LA INTERMEDIA
                        if (datos.Lector["IDESPECIALIDAD"] != DBNull.Value)
                        {
                            aux.Especialidad.Add(new Especialidad
                            {
                                Id = (int)datos.Lector["IDESPECIALIDAD"],
                                Descripcion = (string)datos.Lector["descripcion"]
                            });
                        }
                        lista.Add(aux);
                    }

                    else
                    {
                        if (datos.Lector["IDESPECIALIDAD"] != DBNull.Value)
                        {
                            existente.Especialidad.Add(new Especialidad
                            {
                                Id = (int)datos.Lector["IDESPECIALIDAD"],
                                Descripcion = (string)datos.Lector["descripcion"]
                            });
                        }

                    }

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




        public void agregarMedico(Medico nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Insert en la tabla Medico
                datos.setearConsulta("INSERT INTO Medico (Matricula, Nombre, Apellido, Email, Telefono) OUTPUT INSERTED.IDMedico VALUES (@Matricula, @Nombre, @Apellido, @Email, @Telefono)");
                datos.setearParametros("@Matricula", nuevo.Matricula);
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Apellido", nuevo.Apellido);
                datos.setearParametros("@Email", nuevo.Email);
                datos.setearParametros("@Telefono", nuevo.Telefono);


                int idMedico = (int)datos.ejecutarScalar(); // Obtener el ID generado
                nuevo.IdMedico = idMedico;
                cargarIntermedia(nuevo);

                // Luego de guardar el médico, creamos su usuario Usuario y Contra = a la Matrícula. 
                Usuario nuevoUsuario = new Usuario();
                nuevoUsuario.UserName = nuevo.Matricula.ToString();
                nuevoUsuario.Password = nuevo.Matricula.ToString();
                nuevoUsuario.TipoUsuario = new TipoUsuario();
                nuevoUsuario.TipoUsuario.Id = 3; // ID de tipo Médico

                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                usuarioNegocio.agregarUsuarioMedico(nuevoUsuario);


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

        public void cargarIntermedia(Medico medico)
        {
            AccesoDatos datos = new AccesoDatos();


            try
            {

                // Insertar en tabla intermedia ESPECIALIDADES_MEDICO
                foreach (Especialidad esp in medico.Especialidad)
                {
                    datos = new AccesoDatos();
                    datos.setearConsulta("INSERT INTO ESPECIALIDADES_MEDICOS (IDEspecialidad, IDMedico) VALUES (@idEspecialidad, @idMedico)");
                    datos.setearParametros("@idEspecialidad", esp.Id);
                    datos.setearParametros("@idMedico", medico.IdMedico);
                    datos.ejecutarAccion();
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

        public void modificarMedico(Medico medico)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Actualizar datos del médico 
                datos.setearConsulta("UPDATE Medico SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Telefono = @Telefono WHERE IdMedico = @IdMedico");

                datos.setearParametros("@IdMedico", medico.IdMedico);
                datos.setearParametros("@Nombre", medico.Nombre);
                datos.setearParametros("@Apellido", medico.Apellido);
                datos.setearParametros("@Email", medico.Email);
                datos.setearParametros("@Telefono", medico.Telefono);
                datos.ejecutarAccion();

                // Eliminar especialidades actuales del médico
                datos = new AccesoDatos();
                datos.setearConsulta("DELETE FROM ESPECIALIDADES_MEDICOS WHERE IDMEDICO = @IdMedico");
                datos.setearParametros("@IdMedico", medico.IdMedico);
                datos.ejecutarAccion();
                datos.cerrarConexion();

                //Insertar las nuevas especialidades seleccionadas
                foreach (Especialidad esp in medico.Especialidad)
                {
                    datos = new AccesoDatos();
                    datos.setearConsulta("INSERT INTO ESPECIALIDADES_MEDICOS (IDEspecialidad, IDMedico) VALUES (@idEspecialidad, @idMedico)");
                    datos.setearParametros("@idEspecialidad", esp.Id);
                    datos.setearParametros("@idMedico", medico.IdMedico);
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

        public Medico existeMedico(string Matricula)
        {
            AccesoDatos datos = new AccesoDatos();
            Medico medico = null;

            try
            {
                datos.setearConsulta("SELECT M.idMedico, M.Matricula, M.Nombre, M.Apellido, M.Telefono, M.Email, EM.IDESPECIALIDAD, E.descripcion FROM Medico M LEFT JOIN ESPECIALIDADES_MEDICOS EM ON M.idMedico = EM.IDMEDICO LEFT JOIN Especialidad E ON E.idEspecialidad = EM.IDESPECIALIDAD WHERE M.Matricula = @Matricula");
                datos.setearParametros("@Matricula", Matricula);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    if (medico == null)
                    {
                        medico = new Medico();
                        medico.Matricula = Matricula;
                        medico.IdMedico = (int)datos.Lector["idMedico"];
                        medico.Nombre = datos.Lector["Nombre"].ToString();
                        medico.Apellido = datos.Lector["Apellido"].ToString();
                        medico.Telefono = datos.Lector["Telefono"].ToString();
                        medico.Email = datos.Lector["Email"].ToString();
                        medico.Especialidad = new List<Especialidad>();
                    }


                    if (datos.Lector["IDESPECIALIDAD"] != DBNull.Value)
                    {
                        Especialidad esp = new Especialidad
                        {
                            Id = (int)datos.Lector["IDESPECIALIDAD"],
                            Descripcion = datos.Lector["descripcion"].ToString()
                        };

                        medico.Especialidad.Add(esp);
                    }
                } 
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return medico;
        }

        public Medico existeMedico(int Id)
        {
            AccesoDatos datos = new AccesoDatos();
            Medico medico = null;

            try
            {
                datos.setearConsulta("SELECT M.idMedico, M.Matricula, M.Nombre, M.Apellido, M.Telefono, M.Email, EM.IDESPECIALIDAD, E.descripcion FROM Medico M LEFT JOIN ESPECIALIDADES_MEDICOS EM ON M.idMedico = EM.IDMEDICO LEFT JOIN Especialidad E ON E.idEspecialidad = EM.IDESPECIALIDAD WHERE M.idMedico = @Id");
                datos.setearParametros("@Id", Id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    if (medico == null)
                    {
                        medico = new Medico();
                        medico.IdMedico = Id;
                        medico.Matricula = datos.Lector["Matricula"].ToString();
                        medico.Nombre = datos.Lector["Nombre"].ToString();
                        medico.Apellido = datos.Lector["Apellido"].ToString();
                        medico.Telefono = datos.Lector["Telefono"].ToString();
                        medico.Email = datos.Lector["Email"].ToString();
                        medico.Especialidad = new List<Especialidad>();
                    }


                    if (datos.Lector["IDESPECIALIDAD"] != DBNull.Value)
                    {
                        Especialidad esp = new Especialidad
                        {
                            Id = (int)datos.Lector["IDESPECIALIDAD"],
                            Descripcion = datos.Lector["descripcion"].ToString()
                        };

                        medico.Especialidad.Add(esp);
                    }
                }
                return medico;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminarMedico(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Borramos de manera lógica la tabla intermedia que asocia ID médico con especialidades
                datos.setearConsulta("UPDATE Medico SET Activo = 0 WHERE IdMedico = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();
                datos.cerrarConexion();

                // Borramos de manera lógica la tabla intermedia que asocia ID médico con especialidades
                datos = new AccesoDatos();
                datos.setearConsulta("UPDATE ESPECIALIDADES_MEDICOS SET Activo = 0 WHERE IdMedico = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();

                //EN EL FUTURO DEBERÍAMOS BORRAR LOS TURNOS ASIGNADOS TAMBIÉN. 
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
