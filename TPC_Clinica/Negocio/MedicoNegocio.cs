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
                datos.setearConsulta("INSERT INTO Medicos (Matricula, Nombre, Apellido, Especialidad, Email, Telefono) values (@Matricula, @Nombre, @Apellido, @Especialidad, @Email, @Telefono)");

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
                datos.setearConsulta("Update Medicos set Nombre = @Nombre, Apellido = @Apellido, Especialidad = @Especialidad, Email = @Email, Telefono = @Telefono where Matricula = @Matricula");

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

    }
}
