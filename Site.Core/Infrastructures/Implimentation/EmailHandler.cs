using Microsoft.AspNetCore.Identity.UI.Services;
using Site.Core.Infrastructures.Interfaces;
using System.Threading.Tasks;

namespace Site.Core.Infrastructures.Implimentation
{
    public class EmailHandler : IEmailHandler
    {
        public EmailHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public IEmailSender _emailSender { get; set; }

        public async Task SendEmailAsync(string Email, string Subject, string HtmlMessage)
        {
            await _emailSender.SendEmailAsync(Email, Subject,HtmlMessage);
        }
    }
}
