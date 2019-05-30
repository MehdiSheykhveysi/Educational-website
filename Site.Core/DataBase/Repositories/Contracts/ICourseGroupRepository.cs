using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public interface ICourseGroupRepository : IGenericRepositories<CourseGroup>
    {
        Task<List<CourseGroupDTO>> GetGroupWithSubGroupAsync(CancellationToken cancellationToken, string GroupName, bool IsDeleted = false);
    }
}