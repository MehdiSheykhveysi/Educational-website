using Site.Web.Models.HomeViewModel;
using System.Collections.Generic;

namespace Site.Web.Infrastructures.Compares
{
    public class CourseGroupCompare : IEqualityComparer<CourseGroupVm>
    {
        public bool Equals(CourseGroupVm x, CourseGroupVm y)
        {
            if (x.Id == y.Id)
                return true;
            else
                return false;
        }

        public int GetHashCode(CourseGroupVm obj)
        {
            return obj.ToString().ToLower().GetHashCode();
        }
    }
}
