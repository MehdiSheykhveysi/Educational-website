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
    public class TransactRepository : GenericRepositories<Transact>, ITransactRepository
    {
        public TransactRepository(LearningSiteDbContext context) : base(context)
        {

        }

        public async Task<List<Transact>> GetWalletByUserIdAsync(Guid UserId, CancellationToken cancellationToken)
        {
            List<Transact> query = await NoTrackEntities.
                Where(t => t.CustomUserId == UserId).
                AsNoTracking().
                ToListAsync(cancellationToken);
            return query;
        }
    }
}
