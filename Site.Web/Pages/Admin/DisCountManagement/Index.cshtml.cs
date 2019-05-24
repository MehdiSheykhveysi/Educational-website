using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using Site.Web.Infrastructures;
using Site.Web.Models.PagesModels.DisCountModel;

namespace Site.Web.Pages.Admin.DisCountManagement
{
    public class IndexModel : PageModel
    {
        public IndexModel(IDisCountRepository DisCountRepository)
        {
            disCountRepository = DisCountRepository;
        }

        private readonly IDisCountRepository disCountRepository;

        public IEnumerable<DisCountIndexVmModel> Model { get; set; }

        public async Task OnGetAsync(CancellationToken cancellationToken, string DisCountname = null)
        {
            List<DisCount> list = await disCountRepository.GetAllAsync(DisCountname, cancellationToken);
            ViewData["SearchKey"] = DisCountname;
            Model = list?.Select(d => new DisCountIndexVmModel
            {
                Id = d.Id,
                Title = d.Title,
                Count = d.Count,
                DisCountPercent = d.DisCountPercent,
                MaxDate = d.MaxDate.ToShamsi(),
                StartDate = d.StartDate.ToShamsi()
            });
        }
    }
}