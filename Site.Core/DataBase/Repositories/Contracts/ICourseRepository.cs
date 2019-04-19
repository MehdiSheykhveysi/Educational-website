using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.DTO;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public interface ICourseRepository : IGenericRepositories<Course>
    {
        Task<PagedResult<Course>> GetPagedCourseAsync(string Title, bool IsDeleted, int Count, int CurrentNumber, CancellationToken cancellationToken);
        Task<Course> GetCourseWithKeyWordsAsync(int Id, CancellationToken cancellationToken);
    }
}