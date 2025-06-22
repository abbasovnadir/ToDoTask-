using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Interfaces.Services.ApplicationServices;
using System.Net;
using System.Net.Mail;

namespace ToDoApp.BackEnd.Infrastructure.Services.ApplicationServices
{
    public class EmailServices : IEmailServices
    {
        public void SendEmail(SendMailDto sendMailDto)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(sendMailDto.MailParameters.Email);
                mail.To.Add(sendMailDto.senderEmail);
                mail.Subject = sendMailDto.subject;
                mail.Body = sendMailDto.body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(sendMailDto.MailParameters.SMTP))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(sendMailDto.MailParameters.Email, sendMailDto.MailParameters.Password);
                    smtp.EnableSsl = sendMailDto.MailParameters.SSL;
                    smtp.Port = sendMailDto.MailParameters.Port;
                    smtp.Send(mail);
                }
            }
        }
    }
}
