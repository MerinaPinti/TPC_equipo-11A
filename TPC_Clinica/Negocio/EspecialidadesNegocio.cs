using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
   public class EspecialidadesNegocio
    {
        public List<Especialidades> Listar()
        {
            List<Especialidades> lista = new List<Especialidades>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT ID, Nombre FROM ESPECIALIDADES ORDER BY Nombre");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Especialidades esp = new Especialidades();
                    esp.ID = (int)datos.Lector["ID"];
                    esp.Nombre = (string)datos.Lector["Nombre"];

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
    }
    }

