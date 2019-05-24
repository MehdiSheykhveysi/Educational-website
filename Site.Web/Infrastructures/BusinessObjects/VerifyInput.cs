using Site.Core.Domain.Entities;

namespace Site.Web.Infrastructures.BusinessObjects
{
    public class VerifyInput
    {
        public string Status { get; set; }
        public string Token { get; set; }
    }
}
