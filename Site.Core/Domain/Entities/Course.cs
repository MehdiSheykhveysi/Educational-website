using System;
using System.Collections.Generic;

namespace Site.Core.Domain.Entities
{
    public class Course : BaseEntity
    {
        public Course()
        {

        }
        public Course(DateTime CreateDate)
        {
            this.CreateDate = CreateDate;
        }

        public string CourseTitle { get; set; }

        public string CourseDescription { get; set; }

        public decimal CoursePrice { get; set; }

        public string ImageName { get; set; }

        public string DemoFileName { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public TimeSpan TotalEpisodTime { get; set; }

        public bool IsDeleted { get; set; }

        //Foreign key
        public int? CourseStatusId { get; set; }
        public int? CourseLevelId { get; set; }
        public Guid? CustomUserId { get; set; }
        public int? CourseGroupId { get; set; }

        //Navigations
        public CourseStatus CourseStatus { get; set; }
        public CourseLevel CourseLevel { get; set; }
        public CustomUser CustomUser { get; set; }
        public CourseGroup CourseGroup { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<CourseEpisod> CourseEpisods { get; set; }
        public ICollection<Keyword> Keywordkeys { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
