using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.DTO;
using Site.Core.Infrastructures.Utilities.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public interface ICourseRepository : IGenericRepositories<Course>
    {
        Task<PagedResult<Course>> GetPagedCourseAsync(string Title, bool IsDeleted, int Count, int CurrentNumber, CancellationToken cancellationToken, PriceStatusType selectBy = PriceStatusType.All, OrderStatusType orderBy = OrderStatusType.Default, int StartingPrice = 0, int EndOfPrice = 0, IEnumerable<int> SelectedGroup = null, string KeyWordTitle = "");
        Task<Course> GetCourseWithKeyWordsAsync(int Id, CancellationToken cancellationToken);
        Task<CourseDetailDTO> GetCourseDetailAsync(int CourseId, CancellationToken cancellationToken);
    }
}