using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using Site.Web.Infrastructures;
using Site.Web.Models.PagesModels.DisCountModel;

namespace Site.Web.Pages.Admin.DisCountManagement
{
    public class EditModel : PageModel
    {
        private readonly IDisCountRepository disCountRepository;

        [BindProperty]
        public DisCountEditVm Model { get; set; }

        public EditModel(IDisCountRepository DisCountRepository)
        {
            disCountRepository = DisCountRepository;
        }


        public async Task OnGetAsync(int Id, CancellationToken CancellationToken)
        {
            DisCount selectedDisCount = await disCountRepository.GetByIdAsync(Id, CancellationToken);

            Model = new DisCountEditVm
            {
                Id = selectedDisCount.Id,
                Count = selectedDisCount.Count,
                DisCountPercent = selectedDisCount.DisCountPercent,
                MaxDate = selectedDisCount.MaxDate.ToShamsi(),
                StartDate = selectedDisCount.StartDate.ToShamsi(),
                Title = selectedDisCount.Title
            };
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken CancellationToken)
        {

            DisCount disCount = new DisCount
            {
                Id = Model.Id,
                Count = Model.Count,
                DisCountPercent = Model.DisCountPercent,
                MaxDate = Model.MaxDate.ToGregorian(),
                StartDate = Model.StartDate.ToGregorian(),
                Title = Model.Title
            };

            await disCountRepository.UpdateAsync(disCount, CancellationToken);

            return RedirectToPage("/Admin/DisCountManagement/Index");
        }
    }
}