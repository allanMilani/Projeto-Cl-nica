using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Projeto_Cliínica.Helpers
{
    public static class Email
    {
        public static bool SendEmail(string to, string subject, string message) {
            try
            {
                MailMessage _mailMessage = new MailMessage();
                //Remetente
                _mailMessage.From = new MailAddress("projeto.clinica21@gmail.com");

                //Construindo Mensagem
                _mailMessage.CC.Add(to);
                _mailMessage.Subject = subject;
                _mailMessage.IsBodyHtml = true;
                _mailMessage.Body = message;

                //Configurações SMTP
                SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));
                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential("projeto.clinica21@gmail.com", "projetoClinica2021");
                _smtpClient.EnableSsl = true;
                _smtpClient.Send(_mailMessage);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
