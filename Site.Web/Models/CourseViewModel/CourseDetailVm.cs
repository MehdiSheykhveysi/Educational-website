using Site.Core.Infrastructures.DTO;

namespace Site.Web.Models.CourseViewModel
{
    public class CourseDetailVm
    {
        public CourseDetailDTO CourseDetail { get; set; }

        public PagedResult<CommentDTO> PagedComment { get; set; }
    }
}
