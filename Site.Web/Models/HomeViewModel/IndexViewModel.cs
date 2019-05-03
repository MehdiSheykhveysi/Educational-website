using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.DTO;

namespace Site.Web.Models.HomeViewModel
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.PagedResult = new PagedResult<Course>();
        }
        public PagedResult<Course> PagedResult { get; set; }

        public int PageNumber { get; set; } = 1;
        public string Searchkeyvalue { get; set; }
    }

    public class CourseGroupVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Checked { get; set; }
    }
}
