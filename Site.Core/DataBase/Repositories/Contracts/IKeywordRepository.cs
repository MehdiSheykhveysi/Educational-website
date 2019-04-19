using Site.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public interface IKeywordRepository : IGenericRepositories<Keyword>
    {
        Task<List<Keyword>> GetKeywordkeys(CancellationToken cancellationToken, string Keyword = null);
    }
}