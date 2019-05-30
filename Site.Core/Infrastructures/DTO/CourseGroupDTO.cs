using System.Collections.Generic;

namespace Site.Core.Infrastructures.DTO
{
    public class CourseGroupDTO
    {
        public int ID { get; set; }

        public int? ParentId { get; set; }

        public string ParentTitle { get; set; }

        public List<SubCourseGropDTO> Groups { get; set; }
    }
}