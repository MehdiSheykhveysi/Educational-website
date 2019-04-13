using Site.Core.DataBase.Context;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Repositories
{
    public class CourseLevelRepository : GenericRepositories<CourseLevel>, ICourseLevelRepository
    {
        public CourseLevelRepository(LearningSiteDbContext context) : base(context)
        {

        }
    }
}
