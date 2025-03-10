using Domain.Common;
using Domain.Entities;
using EntityFramework;

namespace Database.Commands
{
    public abstract class DbCommandsBase<TEntity> where TEntity : IAuditableEntity
    {
        protected readonly AppDbContext _dbContext;

        protected DbCommandsBase(AppDbContext finDbContext)
        {
            _dbContext = finDbContext;
        }

        public async Task UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            TEntity oldEntity = null;

            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateRangeEntitiesAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            _dbContext.UpdateRange(entities);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task InsertEntitiesAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                await _dbContext.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            }

            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<Guid> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return entity.Id;
        }
        protected virtual Task MergeAsync(AppDbContext dbContext, TEntity currentEntity, TEntity dbEntity, CancellationToken cancellationToken = default)
        {
            if (currentEntity is IAuditableEntity efEntity && dbEntity is IAuditableEntity dbEfEntity)
                efEntity.CreatedOn = dbEfEntity.CreatedOn;

            currentEntity.Id = dbEntity.Id;
            return Task.CompletedTask;
        }
    }
}
