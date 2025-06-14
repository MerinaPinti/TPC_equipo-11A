using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MedicoNegocio
    {
        public void agregarMedico(Medico nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Medico (Matricula, Nombre, Apellido, Especialidad, Email, Telefono) values (@Matricula, @Nombre, @Apellido, @Especialidad, @Email, @Telefono)");

                datos.setearParametros("@Matricula", nuevo.Matricula);
                datos.setearParametros("@Nombre ", nuevo.Nombre);
                datos.setearParametros("@Apellido ", nuevo.Apellido);
                datos.setearParametros("@Especialidad ", nuevo.Especialidad);
                datos.setearParametros("@Email ", nuevo.Email);
                datos.setearParametros("@Telefono ", nuevo.Telefono);

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
