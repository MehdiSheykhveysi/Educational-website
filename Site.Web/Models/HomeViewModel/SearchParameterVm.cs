using Site.Core.Infrastructures.Utilities.Enums;
using System.Collections.Generic;

namespace Site.Web.Models.HomeViewModel
{
    public class SearchParameterVm
    {
        public SearchParameterVm()
        {
            this.CourseGroups = new List<CourseGroupVm>();
        }

        public PriceStatusType PriceStatusType { get; set; }
        public OrderStatusType OrderStatusType { get; set; }
        public int StartingPrice { get; set; } = 0;
        public int EndOfPrice { get; set; } = 0;
        public List<CourseGroupVm> CourseGroups { get; set; }
    }
}
