using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.DTO;
using Site.Web.Models.HomeViewModel;
using System.Collections.Generic;

namespace Site.Web.Models.CourseViewModel
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.PagedResult = new PagedResult<Course>();
            this.SearchParameter = new SearchParameterVm();
        }
        public PagedResult<Course> PagedResult { get; set; }

        public int PageNumber { get; set; } = 1;
        public string Searchkeyvalue { get; set; }
        public SearchParameterVm SearchParameter { get; set; }
    }
}
