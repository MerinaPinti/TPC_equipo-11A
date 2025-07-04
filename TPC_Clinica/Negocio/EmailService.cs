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

        public EmailService() //GMAIL
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("clinicamedicameraki@gmail.com", "yfsyxonjlbamovxg");
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "smtp.gmail.com";
        }
        /*
        public EmailService() //MAILTRAP
        {
            server = new SmtpClient("sandbox.smtp.mailtrap.io", 2525);
            server.Credentials = new NetworkCredential("f8e02177848b48", "acbac658d54cae");
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "sandbox.smtp.mailtrap.io";
        }*/

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
