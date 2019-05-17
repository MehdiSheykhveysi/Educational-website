using System;
using System.Collections.Generic;

namespace Site.Core.Infrastructures.DTO
{
    public class CourseDetailDTO
    {
        public int CourseID { get; set; }

        public int OrderCount { get; set; }

        public string CourseTitle { get; set; }

        public string CourseDescription { get; set; }

        public decimal CoursePrice { get; set; }

        public string ImageName { get; set; }

        public string DemoFileName { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public TimeSpan TotalEpisodTime { get; set; }

        public IEnumerable<EpisodDTO> Episods { get; set; }

        public IEnumerable<KeywordDTO> Keywords { get; set; }

        public string CourseStatusTitle { get; set; }

        public string CourseLevelTitle { get; set; }

        public string TeacherUserName { get; set; }

        public Guid TeacherID { get; set; }

        public string TeacherAvatar { get; set; }
    }

    public class EpisodDTO
    {
        public string EpisodTitle { get; set; }

        public int EpisodID { get; set; }

        public TimeSpan EpisodeTime { get; set; }

        public bool IsFree { get; set; }
    }

    public class KeywordDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
