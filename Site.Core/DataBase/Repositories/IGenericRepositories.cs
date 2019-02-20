using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Repositories
{
    public interface IGenericRepositories<TEntity> where TEntity : class, IEntity
    {
        DbSet<TEntity> Entities { get; set; }
        IQueryable<TEntity> NoTrackEntities { get; }
        IQueryable<TEntity> TrackEntities { get; }

        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(object ID, CancellationToken CancellationToken);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    }
}