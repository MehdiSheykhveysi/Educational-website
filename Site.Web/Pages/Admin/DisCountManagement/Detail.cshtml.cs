using System;
using System.Collections.Generic;
using System.Linq;
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
    public class DetailModel : PageModel
    {
        private readonly IDisCountRepository disCountRepository;

        public DisCountEditVm Model { get; set; }

        public DetailModel(IDisCountRepository DisCountRepository)
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
    }
}