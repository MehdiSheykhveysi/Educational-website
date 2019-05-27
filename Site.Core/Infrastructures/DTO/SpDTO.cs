using System;

namespace Site.Core.Infrastructures.DTO
{
    public class SpDTO
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public decimal CoursePrice { get; set; }
        public string ImageName { get; set; }
        public TimeSpan TotalEpisodTime { get; set; }
    }
}
