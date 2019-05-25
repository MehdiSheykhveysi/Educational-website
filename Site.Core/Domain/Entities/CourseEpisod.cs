using System;

namespace Site.Core.Domain.Entities
{
    public class CourseEpisod : BaseEntity
    {
        public string Title { get; set; }

        public TimeSpan EpisodeTime { get; set; }

        public string FileName { get; set; }

        public bool IsFree { get; set; }

        //Foreign key
        public int CourseId { get; set; }

        //Navigation Property
        public Course Course { get; set; }
    }
}
