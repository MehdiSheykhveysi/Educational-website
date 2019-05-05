using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.DTO;
using Site.Web.Models.HomeViewModel;

namespace Site.Web.Models
{
    public class PagingViewModel
    {
        public PagingViewModel()
        {
            this.PagedResult = new PagedResult<Course>();
            this.SearchParameter = new SearchParameterVm();
        }
        public PagedResult<Course> PagedResult { get; set; }

        public SearchParameterVm SearchParameter { get; set; }
    }
}
