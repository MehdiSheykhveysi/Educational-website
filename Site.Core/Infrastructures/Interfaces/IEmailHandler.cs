using System.Threading.Tasks;

namespace Site.Core.Infrastructures.Interfaces
{
    public interface IEmailHandler
    {
        Task SendEmailAsync(string Email, string Subject, string HtmlMessage);
    }
}
