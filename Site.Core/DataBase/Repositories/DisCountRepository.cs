using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Context;
using Site.Core.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public class DisCountRepository : GenericRepositories<DisCount>, IDisCountRepository
    {
        public DisCountRepository(LearningSiteDbContext context) : base(context)
        {

        }

        public bool CheckExistEntity(string Title) => NoTrackEntities.Any(d => d.Title == Title);

        public async Task<DisCount> GetByTitle(string Title, CancellationToken cancellationToken)
        {
            return await NoTrackEntities.FirstOrDefaultAsync(d => EF.Functions.Like(d.Title, $"{Title}"), cancellationToken);
        }
    }
}
