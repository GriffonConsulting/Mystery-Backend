using Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Common.Interfaces.Repositories;

public interface IRepository<TEntity> : IDisposable where TEntity : IAuditableEntity
{

    public Task UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken = default);

    public Task UpdateRangeEntitiesAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    public Task InsertEntitiesAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    public Task<Guid> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    public Task<TEntity[]> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<TEntity[]> GetAllByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    public ValueTask<TEntity> GetById(params object[] keyValues);

    public ValueTask<TEntity> GetById(object[] keyValues, CancellationToken cancellationToken = default);

    public bool IsExist(Expression<Func<TEntity, bool>> predicate);

    public Task<TEntity[]> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    public IQueryable<TEntity> AllAsQueryable();

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
}