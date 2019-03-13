using Microsoft.AspNetCore.Mvc;

namespace Site.Web.Models.PagesModels
{
    public class AdminEditModel : BaseViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        public string Avatar { get; set; }
    }
}