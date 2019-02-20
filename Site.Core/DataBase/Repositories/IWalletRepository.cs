using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Repositories
{
    public interface IWalletRepository: IGenericRepositories<Wallet>
    {
        Task<List<Wallet>> GetWalletByUserId(object UserId, CancellationToken cancellationToken,bool IsConfitmPayFlag = true);
    }
}