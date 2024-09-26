using Mvc.Demo.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Mvc03.Demo.PL.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            //Mail Servaer:gmail.com
            //smtp
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("marwa@bluskyint.com", "wdddlpmnkvixnpbg");
                client.Send("marwa@bluskyint.com", email.To, email.Subject, email.Body);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"SMTP Exception: {ex.Message}");
                throw;
            }

        }
    }
}
