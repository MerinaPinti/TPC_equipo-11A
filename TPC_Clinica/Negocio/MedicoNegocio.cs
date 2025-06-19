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

                datos.setearConsulta("SELECT M.email, M.telefono, M.nombre, M.apellido, M.matricula, EM.IDMEDICO, EM.IDESPECIALIDAD, E.descripcion FROM MEDICO as M INNER JOIN ESPECIALIDADES_MEDICOS AS EM ON EM.IDMEDICO = M.idMedico INNER JOIN ESPECIALIDAD AS E ON E.idEspecialidad = EM.idEspecialidad WHERE M.activo = 1;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    int idMedico = (int)datos.Lector["IDMEDICO"];
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
                datos.setearConsulta("Update Medico set Nombre = @Nombre, Apellido = @Apellido, Especialidad = @Especialidad, Email = @Email, Telefono = @Telefono where Matricula = @Matricula");

                datos.setearParametros("@Matricula", medico.Matricula);
                datos.setearParametros("@Nombre", medico.Nombre);
                datos.setearParametros("@Apellido", medico.Apellido);
                datos.setearParametros("@Especialidad", medico.Especialidad);
                datos.setearParametros("@Email", medico.Email);
                datos.setearParametros("@Telefono", medico.Telefono);

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

        public Medico existeMedico(string Matricula)
        {
            AccesoDatos datos = new AccesoDatos();
            Medico medico = null;

            try
            {
                datos.setearConsulta("SELECT Matricula, Nombre, Apellido, Telefono, Email, idEspecialidad FROM Medico WHERE Matricula = @Matricula");
                datos.setearParametros("@Matricula", Matricula);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    medico = new Medico();
                    medico.Matricula = datos.Lector["Matricula"].ToString();
                    medico.Nombre = datos.Lector["Nombre"].ToString();
                    medico.Apellido = datos.Lector["Apellido"].ToString();
                    medico.Telefono = datos.Lector["Telefono"].ToString();
                    medico.Email = datos.Lector["Email"].ToString();

                }

            }
            catch (Exception)
            {

                throw;
            }

            return medico;
        }

    }
}
