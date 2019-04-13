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
    public class KeywordRepository : GenericRepositories<Keywordkey>, IKeywordRepository
    {
        public KeywordRepository(LearningSiteDbContext context) : base(context)
        {

        }

        public async Task<List<Keywordkey>> GetKeywordkeys(CancellationToken cancellationToken, string Keyword = null)
        {
            return await NoTrackEntities.Where(k => string.IsNullOrEmpty(Keyword) || k.Title.Equals(Keyword, StringComparison.CurrentCultureIgnoreCase)).ToListAsync(cancellationToken);
        }
    }
}
