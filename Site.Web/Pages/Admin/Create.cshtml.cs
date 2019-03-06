using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.Domain.Entities;
using Site.Web.Infrastructures.Interfaces;
using Site.Web.Models.PagesModels;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Pages.Admin
{
    public class CreateModel : PageModel
    {
        public CreateModel(UserManager<CustomUser> UserManager, IHostingEnvironment HostingEnvironment, IImageHandler ImageHandler, IMapper Mapper)
        {
            userManager = UserManager;
            hostingEnvironment = HostingEnvironment;
            imageHandler = ImageHandler;
            mapper = Mapper;
        }

        private readonly IMapper mapper;
        private readonly IImageHandler imageHandler;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<CustomUser> userManager;

        [BindProperty]
        public AdminCreateModel Model { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                CustomUser user = new CustomUser();
                user = mapper.Map(Model, user);
                string uploads = Path.Combine(hostingEnvironment.WebRootPath, "images", "UserProfile");
                string strFilePath = await imageHandler.UploadImageAsync(Model.FormFile, uploads, cancellationToken);
                IdentityResult result = await userManager.CreateAsync(user, Model.PassWord);

                if (result.Succeeded)
                {
                    return RedirectToPage("/Admin/index");
                }
                else
                {
                    foreach (IdentityError item in result.Errors)
                    {
                        ModelState.TryAddModelError(item.Code, item.Description);
                    }
                }
            }
            return Page();
        }
    }
}