namespace Site.Web.Areas.User.Models.HomeModels
{
    public class UserProfileViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string RegisterDate { get; set; }
        public decimal Wallet { get; set; }
        public string UserProfileUrl { get; set; }
    }
}
