using Site.Core.DataBase.Context;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Repositories
{
    public class CourseGroupRepository : GenericRepositories<CourseGroup>, ICourseGroupRepository
    {
        public CourseGroupRepository(LearningSiteDbContext context) : base(context)
        {

        }
    }
}
