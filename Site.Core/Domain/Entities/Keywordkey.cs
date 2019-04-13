using System.Collections.Generic;

namespace Site.Core.Domain.Entities
{
    public class Keywordkey : BaseEntity
    {
        public string Title { get; set; }

        //Foreign key
        public int? ParentKeywordkeyId { get; set; }
        public int? CourseId { get; set; }

        //Relations
        public Course Course { get; set; }
        public Keywordkey ParentKeywordkey { get; set; }
        public ICollection<Keywordkey> Keywordkeys { get; set; }
    }
}
