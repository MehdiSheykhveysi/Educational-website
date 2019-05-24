using Site.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public interface IDisCountRepository : IGenericRepositories<DisCount>
    {
        bool CheckExistEntity(string Title);
        Task<DisCount> GetByTitle(string Title, CancellationToken cancellationToken);
        Task<List<DisCount>> GetAllAsync(string DisCountname, CancellationToken cancellationToken);
    }
}