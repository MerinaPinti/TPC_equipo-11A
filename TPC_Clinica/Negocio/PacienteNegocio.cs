using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Negocio;

namespace Negocio
{
    public class PacienteNegocio
    {
        public void agregarPaciente(Paciente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Paciente (DNI, Nombre, Apellido, FechaNac, Telefono, Email, Direccion) values (@DNI, @Nombre, @Apellido, @FechaNac, @Telefono, @Email, @Direccion)");
                datos.setearParametros("@DNI", nuevo.DNI);
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Apellido", nuevo.Apellido);
                datos.setearParametros("@FechaNac", nuevo.FechaNac);
                datos.setearParametros("@Telefono", nuevo.Telefono);
                datos.setearParametros("@Email", nuevo.Email);
                datos.setearParametros("@Direccion", nuevo.Direccion);

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

        public void modificarPaciente(Paciente paciente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Update Paciente set Nombre = @Nombre, Apellido = @Apellido, FechaNac = @FechaNac, Telefono = @Telefono, Email = @Email, Direccion = @Direccion, Activo = 1 WHERE DNI = @DNI");

                datos.setearParametros("@DNI", paciente.DNI);
                datos.setearParametros("@Nombre", paciente.Nombre);
                datos.setearParametros("@Apellido", paciente.Apellido);
                datos.setearParametros("@FechaNac", paciente.FechaNac);
                datos.setearParametros("@Telefono", paciente.Telefono);
                datos.setearParametros("@Email", paciente.Email);
                datos.setearParametros("@Direccion", paciente.Direccion);

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

        public Paciente existePaciente(string DNI)
        {
            AccesoDatos datos = new AccesoDatos();
            Paciente paciente = null;

            try
            {
                datos.setearConsulta("SELECT idPaciente, DNI, Nombre, Apellido, FechaNac, Telefono, Email, Direccion FROM Paciente WHERE DNI = @DNI");
                datos.setearParametros("@DNI", DNI);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    paciente = new Paciente();
                    paciente.IdPaciente = (int)datos.Lector["idPaciente"]; 
                    paciente.DNI = datos.Lector["DNI"].ToString();
                    paciente.Nombre = datos.Lector["Nombre"].ToString();
                    paciente.Apellido = datos.Lector["Apellido"].ToString();
                    paciente.FechaNac = (DateTime)datos.Lector["FechaNac"];
                    paciente.Telefono = datos.Lector["Telefono"].ToString();
                    paciente.Email = datos.Lector["Email"].ToString();
                    paciente.Direccion = datos.Lector["Direccion"].ToString();
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

            return paciente;
        }

        public List<Paciente> listarPacientes()
        {
            List<Paciente> lista = new List<Paciente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT DNI, Nombre, Apellido, FechaNac, Telefono, Email, Direccion, Activo FROM Paciente WHERE Activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Paciente paciente = new Paciente();
                    paciente.DNI = datos.Lector["DNI"].ToString();
                    paciente.Nombre = datos.Lector["Nombre"].ToString();
                    paciente.Apellido = datos.Lector["Apellido"].ToString();
                    paciente.FechaNac = (DateTime)datos.Lector["FechaNac"];
                    paciente.Telefono = datos.Lector["Telefono"].ToString();
                    paciente.Email = datos.Lector["Email"].ToString();
                    paciente.Direccion = datos.Lector["Direccion"].ToString();
                    paciente.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(paciente);
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

        public List<Paciente> listarPacientesPorMedico(int idMedico)
        {
            List<Paciente> lista = new List<Paciente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT DISTINCT P.nombre, P.apellido, P.DNI, P.fechaNac FROM TURNO T INNER JOIN Paciente P ON T.idPaciente = P.idPaciente WHERE T.idMedico = @idMedico AND T.activo = 1 AND P.activo = 1");
                datos.setearParametros("@idMedico", idMedico);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Paciente paciente = new Paciente();
                    paciente.DNI = datos.Lector["DNI"].ToString();
                    paciente.Nombre = datos.Lector["Nombre"].ToString();
                    paciente.Apellido = datos.Lector["Apellido"].ToString();
                    paciente.FechaNac = (DateTime)datos.Lector["FechaNac"];
                    lista.Add(paciente);
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

        public void eliminarLogico(string DNI)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();

                datos.setearConsulta("UPDATE Paciente set Activo = 0 WHERE DNI = @DNI");

                datos.setearParametros("DNI", DNI);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        


    }
}