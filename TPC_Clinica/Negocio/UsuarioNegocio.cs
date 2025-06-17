using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Negocio;


namespace Negocio
{
    public class UsuarioNegocio
    {
        public void agregarUsuario(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Usuario (idUsuario, idTipoUsuario, usuario, contraseña) values (@TipoUsuario, @UserName, @Password)");
                datos.setearParametros("@TipoUsuario", nuevo.TipoUsuario);
                datos.setearParametros("@UserName", nuevo.UserName);
                datos.setearParametros("@Password", nuevo.Password);

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

