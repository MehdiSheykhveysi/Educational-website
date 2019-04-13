using Site.Core.DataBase.Context;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Repositories
{
    public class CourseeStatusRepositoty : GenericRepositories<CourseStatus>, ICourseStatusRepositoty
    {
        public CourseeStatusRepositoty(LearningSiteDbContext context) : base(context)
        {

        }
    }
}
