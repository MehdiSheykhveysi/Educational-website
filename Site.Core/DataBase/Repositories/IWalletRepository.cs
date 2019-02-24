using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Repositories
{
    public interface IWalletRepository: IGenericRepositories<Transact>
    {
        Task<List<Transact>> GetWalletByUserId(Guid UserId, CancellationToken cancellationToken,bool IsConfitmPayFlag = true);
    }
}