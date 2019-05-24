using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using Site.Web.Infrastructures;
using Site.Web.Models.PagesModels.DisCountModel;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Pages.Admin.DisCountManagement
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public DisCountCreateVm Model { get; set; }

        private readonly IDisCountRepository disCountRepository;

        public CreateModel(IDisCountRepository DisCountRepository)
        {
            this.disCountRepository = DisCountRepository;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if (disCountRepository.CheckExistEntity(Model.Title.Trim()))
            {
                ModelState.AddModelError("", "قبلا با این نام کد تخفیفی را وارد کرده اید");
                return Page();
            }

            DisCount disCount = new DisCount
            {
                Count = Model.Count,
                DisCountPercent = Model.DisCountPercent,
                MaxDate = Model.MaxDate.ToGregorian(),
                StartDate = Model.StartDate.ToGregorian(),
                Title = Model.Title
            };

            await disCountRepository.AddAsync(disCount, cancellationToken);

            return RedirectToPage("/Admin/DisCountManagement/Index");
        }
    }
}