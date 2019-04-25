using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Context;
using Site.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public class CourseEpisodRepository : GenericRepositories<CourseEpisod>, ICourseEpisodRepository
    {
        public CourseEpisodRepository(LearningSiteDbContext context) : base(context)
        {

        }

        public async Task<List<CourseEpisod>> GetCoursesAsync(int CourseId, CancellationToken cancellationToken)
        {
            return await Entities.Where(e => e.CourseId.Equals(CourseId)).AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
