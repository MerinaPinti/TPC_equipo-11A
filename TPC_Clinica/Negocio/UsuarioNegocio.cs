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
                datos.setearConsulta("SELECT idUsuario AS Id, U.usuario, U.contraseña, U.idTipoUsuario, TU.descripcion, U.activo FROM USUARIO U INNER JOIN TipoUsuario TU ON TU.idTipoUsuario = U.idTipoUsuario\r\n");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario user = new Usuario();
                    user.TipoUsuario = new TipoUsuario
                    {
                        Id = (int)datos.Lector["idTipoUsuario"],
                        Descripcion = (string)datos.Lector["descripcion"]
                    };
                    user.Id = (int)datos.Lector["Id"];  
                    user.UserName = (string)datos.Lector["usuario"];
                    user.Password = (string)datos.Lector["contraseña"];
                    user.TipoUsuario.Id = (int)datos.Lector["idTipoUsuario"];
                    
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
        public List<Usuario> ListarActivos()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT idUsuario AS Id, U.usuario, U.contraseña, U.idTipoUsuario, TU.descripcion, U.activo FROM USUARIO U INNER JOIN TipoUsuario TU ON TU.idTipoUsuario = U.idTipoUsuario WHERE U.activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario user = new Usuario();
                    user.TipoUsuario = new TipoUsuario
                    {
                        Id = (int)datos.Lector["idTipoUsuario"],
                        Descripcion = (string)datos.Lector["descripcion"]
                    };
                    user.Id = (int)datos.Lector["Id"];  
                    user.UserName = (string)datos.Lector["usuario"];
                    user.Password = (string)datos.Lector["contraseña"];
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
                AccesoDatos datos = new AccesoDatos();
                try
                {
                    datos.setearConsulta("INSERT INTO Usuario (usuario, contraseña, idTipoUsuario) VALUES (@user, @pass, @tipo)");
                    datos.setearParametros("@user", usuario.UserName);
                    datos.setearParametros("@pass", usuario.Password);
                    datos.setearParametros("@tipo", usuario.TipoUsuario.Id);
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


        public void agregarUsuarioMedico(Usuario usuario)
        {
            
                AccesoDatos datos = new AccesoDatos();
                try
                {
                    datos.setearConsulta("INSERT INTO Usuario (usuario, contraseña, idTipoUsuario) VALUES (@user, @pass, @tipo)");
                    datos.setearParametros("@user", usuario.UserName);
                    datos.setearParametros("@pass", usuario.Password);
                    datos.setearParametros("@tipo", usuario.TipoUsuario.Id);
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



        public void modificarUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Usuario SET contraseña = @password, idTipoUsuario = @idTipo WHERE idUsuario = @id");
                datos.setearParametros("@id", usuario.Id);
                datos.setearParametros("@password", usuario.Password);
                datos.setearParametros("@idTipo", usuario.TipoUsuario.Id);
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
                    usuario.TipoUsuario = new TipoUsuario();
                    usuario.TipoUsuario.Id = (int)datos.Lector["idTipoUsuario"]; 
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


        public Usuario Login(string username, string password)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario usuario = null;

            try
            {
                datos.setearConsulta("SELECT idUsuario AS Id, usuario, contraseña, U.idTipoUsuario, TU.descripcion, U.activo FROM Usuario U INNER JOIN TipoUsuario TU ON U.idTipoUsuario = TU.idTipoUsuario  WHERE U.usuario = @user AND U.contraseña = @pass AND U.activo = 1");
                datos.setearParametros("@user", username);
                datos.setearParametros("@pass", password);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario = new Usuario();
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.UserName = (string)datos.Lector["usuario"];
                    usuario.Password = (string)datos.Lector["contraseña"];
                    usuario.TipoUsuario = new TipoUsuario { Id = (int)datos.Lector["idTipoUsuario"], Descripcion = datos.Lector["descripcion"].ToString() };
                    usuario.Activo = (bool)datos.Lector["activo"];
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


        public void EliminarUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Usuario SET Activo = 0 WHERE idUsuario = @id");
                datos.setearParametros("@id", usuario.Id);
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

