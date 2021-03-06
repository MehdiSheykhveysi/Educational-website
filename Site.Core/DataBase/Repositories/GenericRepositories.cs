﻿using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Context;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.Utilities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public class GenericRepositories<TEntity> : IGenericRepositories<TEntity> where TEntity : class, IEntity, new()
    {
        protected readonly LearningSiteDbContext context;
        public DbSet<TEntity> Entities { get; set; }
        public IQueryable<TEntity> NoTrackEntities => Entities.AsNoTracking();
        public IQueryable<TEntity> TrackEntities => Entities;

        public GenericRepositories(LearningSiteDbContext Context)
        {
            context = Context;
            Entities = context.Set<TEntity>();
        }

        public virtual Task<TEntity> GetByIdAsync(object ID, CancellationToken CancellationToken)
        {
            return Entities.FindAsync(new object[] { ID }, CancellationToken);
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (Assert.NotNull(entity))
            {
                await Entities.AddAsync(entity, cancellationToken);
                await SaveChangesAsync(cancellationToken);
            }
        }
        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (Assert.NotNull(entity))
            {
                Entities.Update(entity);
                await SaveChangesAsync(cancellationToken);
            }
        }
        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (Assert.NotNull(entity))
            {
                Entities.Remove(entity);
                await SaveChangesAsync(cancellationToken);
            }
        }

        private async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}