using Site.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public interface IKeywordRepository : IGenericRepositories<Keywordkey>
    {
        Task<List<Keywordkey>> GetKeywordkeys(CancellationToken cancellationToken, string Keyword = null);
    }
}