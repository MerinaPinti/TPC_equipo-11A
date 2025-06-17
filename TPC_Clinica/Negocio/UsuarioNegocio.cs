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

        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT idUsuario AS Id, usuario, contraseña, idTipoUsuario, activo FROM Usuario WHERE activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario user = new Usuario();
                    user.Id = (int)datos.Lector["Id"];  // Esta línea es la clave
                    user.UserName = (string)datos.Lector["usuario"];
                    user.Password = (string)datos.Lector["contraseña"];
                    user.TipoUsuario = datos.Lector["idTipoUsuario"].ToString();
                    user.Activo = (bool)datos.Lector["activo"];

                    lista.Add(user);
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

        public void agregarUsuario(List<Usuario> lista)
        {
            foreach (Usuario usuario in lista)
            {
                AccesoDatos datos = new AccesoDatos(); // Instancia dentro del foreach
                try
                {
                    datos.setearConsulta("INSERT INTO Usuario (usuario, contraseña, idTipoUsuario) VALUES (@user, @pass, @tipo)");
                    datos.setearParametros("@user", usuario.UserName);
                    datos.setearParametros("@pass", usuario.Password);
                    datos.setearParametros("@tipo", int.Parse(usuario.TipoUsuario));
                    datos.ejecutarAccion();
                }
                catch (Exception ex)
                {
                    throw ex; // Podés loguearlo o mostrarlo también
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }
        }

        public void modificarUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Usuario SET contraseña = @password WHERE idUsuario = @id");
                datos.setearParametros("@id", usuario.Id);
                datos.setearParametros("@password", usuario.Password);
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

        public Usuario ListarConId(int id)
        {
            Usuario usuario = new Usuario();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT idUsuario as Id, usuario, contraseña, idTipoUsuario FROM Usuario WHERE idUsuario = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.UserName = (string)datos.Lector["usuario"];
                    usuario.Password = (string)datos.Lector["contraseña"];
                    usuario.TipoUsuario = datos.Lector["idTipoUsuario"].ToString(); // guardás como string
                }

                return usuario;
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

