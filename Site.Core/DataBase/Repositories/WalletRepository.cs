using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Context;
using Site.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public class WalletRepository : GenericRepositories<Wallet>, IWalletRepository
    {
        public WalletRepository(LearningSiteDbContext context) : base(context)
        {

        }

        public async Task<List<Wallet>> GetWalletByUserId(object UserId, CancellationToken cancellationToken, bool IsConfitmPayFlag = true)
        {
            var query = NoTrackEntities.Include(w => w.CustomUser);
            return await query.Where(u => u.CustomUser.Id.Equals(UserId) && u.IsConfitmPayTransaction == IsConfitmPayFlag).AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
