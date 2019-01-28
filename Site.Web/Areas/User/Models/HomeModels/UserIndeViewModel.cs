using System;

namespace Site.Web.Areas.User.HomeModels
{
    public class UserIndeViewModel
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisterDate { get; set; }
        public decimal Wallet { get; set; }
        public string UserProfile { get; set; }
    }
}
