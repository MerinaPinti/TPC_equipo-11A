using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class PacienteNegocio
    {
        public void agregarPaciente(Paciente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO PACIENTES (DNI, Nombre, Apellido, Fecha_Nacimiento, Telefono, Email, Direccion, NroHC) values (@DNI, @Nombre, @Apellido, @Fecha_Nacimiento, @Telefono, @Email, @Direccion, @NroHC)");
                datos.setearParametros("@DNI", nuevo.DNI);
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Apellido", nuevo.Apellido);
                datos.setearParametros("@Fecha_Nacimiento", nuevo.Fecha_Nacimiento);
                datos.setearParametros("@Telefono", nuevo.Telefono);
                datos.setearParametros("@Email", nuevo.Email);
                datos.setearParametros("@Direccion", nuevo.Direccion);
                datos.setearParametros("@NroHC", nuevo.NroHC);

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
                //datos.setearConsulta();

                datos.setearParametros("@DNI", paciente.DNI);
                datos.setearParametros("@Nombre", paciente.Nombre);
                datos.setearParametros("@Apellido", paciente.Apellido);
                datos.setearParametros("@Fecha_Nacimiento", paciente.Fecha_Nacimiento);
                datos.setearParametros("@Telefono", paciente.Telefono);
                datos.setearParametros("@Email", paciente.Email);
                datos.setearParametros("@Direccion", paciente.Direccion);
                datos.setearParametros("@NroHC", paciente.NroHC);

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
