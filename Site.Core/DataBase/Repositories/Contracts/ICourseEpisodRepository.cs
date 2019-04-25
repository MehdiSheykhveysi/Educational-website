using Site.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public interface ICourseEpisodRepository : IGenericRepositories<CourseEpisod>
    {
        Task<List<CourseEpisod>> GetCoursesAsync(int CourseId, CancellationToken cancellationToken);
    }
}