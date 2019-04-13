using System.Collections.Generic;

namespace Site.Core.Domain.Entities
{
    public class CourseGroup: BaseEntity
    {
        public string Title { get; set; }
        public bool IsDeleted { get; set; }

        //Foreign key
        public int? ParentId { get; set; }

        //Navigations Property
        public CourseGroup ParentCourseGroup { get; set; }

        //Relatons => Self Relation
        public ICollection<CourseGroup> Groups { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
