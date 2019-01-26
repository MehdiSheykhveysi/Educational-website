using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Site.Core.Infrastructures
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com")
            {
                UseDefaultCredentials = false,
                Port= 587,
                EnableSsl=true,
                Credentials = new NetworkCredential("m.sheykhveysi4680@gmail.com", "MehdiSheykhveysi4680")
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress("m.sheykhveysi4680@gmail.com", "Test")
            };
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.Body = htmlMessage;
            mailMessage.IsBodyHtml = true;
            return client.SendMailAsync(mailMessage);
        }
        //public static void Send(string to, string subject, string body)
        //{
        //    MailMessage mail = new MailMessage();
        //    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        //    mail.From = new MailAddress("TopLearn.com@gmail.com", "تاپ لرن");
        //    mail.To.Add(to);
        //    mail.Subject = subject;
        //    mail.Body = body;
        //    mail.IsBodyHtml = true;

        //    //System.Net.Mail.Attachment attachment;
        //    // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
        //    // mail.Attachments.Add(attachment);

        //    SmtpServer.Port = 587;
        //    SmtpServer.Credentials = new System.Net.NetworkCredential("TopLearn.com@gmail.com", "****");
        //    SmtpServer.EnableSsl = true;

        //    SmtpServer.Send(mail);

    }
}

