using Site.Core.DataBase.Context;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Repositories
{
    public class CourseEpisodRepository : GenericRepositories<CourseEpisod>, ICourseEpisodRepository
    {
        public CourseEpisodRepository(LearningSiteDbContext context) : base(context)
        {

        }
    }
}
