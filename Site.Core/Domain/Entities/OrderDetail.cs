using System;

namespace Site.Core.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        //Forgion Key
        public int CourseId { get; set; }
        public Guid? OrderId { get; set; }

        //Navigations
        public Course Course { get; set; }
        public Order Order { get; set; }
    }
}
