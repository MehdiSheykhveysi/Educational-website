using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Repositories
{
    public interface ITransactRepository : IGenericRepositories<Transact>
    {
        Task<List<Transact>> GetWalletByUserIdAsync(Guid UserId, CancellationToken cancellationToken);
    }
}