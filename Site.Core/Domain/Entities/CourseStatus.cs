using System.Collections.Generic;

namespace Site.Core.Domain.Entities
{
    public class CourseStatus : BaseEntity
    {
        public string Title { get; set; }

        //Relations
        public ICollection<Course> Courses { get; set; }
    }
}
