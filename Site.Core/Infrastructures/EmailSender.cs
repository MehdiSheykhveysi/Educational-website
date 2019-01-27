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
            SmtpClient client = new SmtpClient("smtp.gmail.com")
            {
                UseDefaultCredentials = false,
                Port= 587,
                EnableSsl=true,
                Credentials = new NetworkCredential("m.sheykhveysi4680@gmail.com", "MehdiSheykhveysi4680")
            };
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("m.sheykhveysi4680@gmail.com", "سلام این ایمیل از طرف مهدی شیخ ویسی برای شما ارسال شده است")
            };
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.Body = htmlMessage;
            mailMessage.IsBodyHtml = true;
            return client.SendMailAsync(mailMessage);
        }
    }
}

