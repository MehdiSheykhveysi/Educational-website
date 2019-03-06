using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Context;
using Site.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public class WalletRepository : GenericRepositories<Transact>, IWalletRepository
    {
        public WalletRepository(LearningSiteDbContext context) : base(context)
        {

        }

        public async Task<List<Transact>> GetWalletByUserId(Guid UserId, CancellationToken cancellationToken)
        {
            List<Transact> query = await NoTrackEntities.
                Where(t => t.CustomUserId == UserId).
                AsNoTracking().
                ToListAsync(cancellationToken);
            return query;
        }
    }
}
