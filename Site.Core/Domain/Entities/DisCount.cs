using System;

namespace Site.Core.Domain.Entities
{
    public class DisCount : BaseEntity
    {
        public string Title { get; set; }
        public int Count { get; set; }
        public int DisCountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime MaxDate { get; set; }
    }
}
