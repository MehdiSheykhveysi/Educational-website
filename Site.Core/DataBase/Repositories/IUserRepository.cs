using System.Threading;
using System.Threading.Tasks;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.DTO;

namespace Site.Core.DataBase.Repositories
{
    public interface IUserRepository
    {
        Task<PagedResult<CustomUser>> GetPagedUserAsync(string UserName,int Count, int CurrentNumber, CancellationToken cancellationToken);
    }
}