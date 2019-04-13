using Site.Core.DataBase.Context;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Repositories
{
    public class CourseRepository : GenericRepositories<Course>, ICourseRepository
    {
        public CourseRepository(LearningSiteDbContext Context) : base(Context)
        {

        }
    }
}
