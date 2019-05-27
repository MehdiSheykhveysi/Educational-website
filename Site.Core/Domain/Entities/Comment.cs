using System;

namespace Site.Core.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Name { get; set; }
        public string Body { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsReadedByAdmin { get; set; }
        public bool IsDeleted { get; set; }

        //Forgion Key
        public int CoourseId { get; set; }
        public Guid UserId { get; set; }


        //Navigations
        public CustomUser User { get; set; }
        public Course Course { get; set; }

    }
}
