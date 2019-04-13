using System;
using System.Collections.Generic;
using System.Text;

namespace Site.Core.Domain.Entities
{
    public class CourseLevel : BaseEntity
    {
        public string Title { get; set; }

        //navigations
        public ICollection<Course> Courses { get; set; }
    }
}
