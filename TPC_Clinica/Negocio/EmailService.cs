using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailService()
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("", "");
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "smtp.gmail.com";
        }

        public void armarCorreo(string destino, string asunto, string cuerpo)
        {
            email = new MailMessage();
            email.From = new MailAddress("gestiondeturnos@clinica.com");
            email.To.Add(destino);
            email.Subject = asunto;
            email.IsBodyHtml = true;
            email.Body = cuerpo; //el html del mail se configura aca hasta que encuentre la forma de levantar la plantilla
        }

        public void enviarCorreo()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
