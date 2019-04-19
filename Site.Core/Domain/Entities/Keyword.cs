using System.Collections.Generic;

namespace Site.Core.Domain.Entities
{
    public class Keyword : BaseEntity
    {
        public string Title { get; set; }

        //Foreign key
        public int? CourseId { get; set; }

        //Relations
        public Course Course { get; set; }
    }
}
