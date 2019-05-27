using Microsoft.AspNetCore.Mvc;
using Site.Core.DataBase.Repositories;
using Site.Core.Infrastructures.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.ViewComponents
{
    [ViewComponent]
    public class GetPopularCourseViewComponent : ViewComponent
    {
        private readonly ISpReaderRepository spReaderRepository;

        public GetPopularCourseViewComponent(ISpReaderRepository SpReaderRepository)
        {
            spReaderRepository = SpReaderRepository;
        }


        public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken)
        {
            string[,] Parameters = new string[1, 2];//
            Parameters[0, 0] = "@TakeCount";
            Parameters[0, 1] = "8";
            List<SpDTO> courses = await spReaderRepository.GetSpsAsync(Parameters, cancellationToken);
            return View(courses);
        }
    }
}
