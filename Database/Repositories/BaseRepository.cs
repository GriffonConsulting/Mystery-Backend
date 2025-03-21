using Database.Extensions;
using Domain.Common;
using EntityFramework;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Database.Commands
{
    public abstract class BaseRepository<TEntity> where TEntity : IAuditableEntity
    {
        protected readonly AppDbContext _dbContext;

        protected BaseRepository(AppDbContext finDbContext)
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



        public IQueryable<TEntity> GetFilteredWithInclude(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            string? sortPropertyName = null,
            int? sortOrder = null)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (include != null)
            {
                query = include(query);
            }

            query = query.Where(predicate);

            if (!string.IsNullOrWhiteSpace(sortPropertyName) && HashProperty(sortPropertyName))
            {
                query = query.OrderByDynamic(sortPropertyName, sortOrder);
            }

            return query;
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public Task<TEntity[]> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<TEntity>().ToArrayAsync(cancellationToken);
        }

        public Task<TEntity[]> GetAllByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<TEntity>().Where(predicate).ToArrayAsync(cancellationToken);
        }

        public ValueTask<TEntity> GetById(params object[] keyValues) => _dbContext.Set<TEntity>().FindAsync(keyValues);

        public ValueTask<TEntity> GetById(object[] keyValues, CancellationToken cancellationToken = default) =>
             _dbContext.Set<TEntity>().FindAsync(keyValues, cancellationToken);

        public bool IsExist(Expression<Func<TEntity, bool>> predicate) => _dbContext.Set<TEntity>().Any(predicate);

        public async Task<TEntity[]> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).ToArrayAsync(cancellationToken);
        }

        public IQueryable<TEntity> AllAsQueryable()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().CountAsync(predicate);
        }

        public async Task<TResult[]> GetFilteredPropertiesAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).Select(selector).ToArrayAsync(cancellationToken);
        }

        private static bool HashProperty(string sortPropertyName) =>
            PropertyHelper.GetPropertyName(PropertyHelper.GetProperty<TEntity>(sortPropertyName)) != null;



        #region IDisposable Support
        private bool _isDisposed = false;

        protected void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _dbContext?.Dispose();
                }

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
