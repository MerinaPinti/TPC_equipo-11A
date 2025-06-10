using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EspecialidadNegocio
    {
        public List<Especialidad> Listar()
        {
            List<Especialidad> lista = new List<Especialidad>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT idEspecialidad as Id, descripcion FROM ESPECIALIDAD");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Especialidad esp = new Especialidad();
                    esp.Id = (int)datos.Lector["Id"];
                    esp.Descripcion = (string)datos.Lector["descripcion"];
                    lista.Add(esp);
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

        public void agregarEspecialidad(List<Especialidad> nuevo)
        {
            AccesoDatos datos;
            foreach (Especialidad item in nuevo)
            {
                datos = new AccesoDatos();
                try
                {
                    datos.setearConsulta("INSERT INTO Especialidad (descripcion) VALUES (@descripcion)");
                    datos.setearParametros("@descripcion", item.Descripcion);
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
}

