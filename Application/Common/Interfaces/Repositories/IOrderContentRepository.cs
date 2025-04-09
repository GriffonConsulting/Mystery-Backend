using Domain.Entities;

namespace Application.Common.Interfaces.Repositories
{
    public interface IOrderContentRepository : IRepository<OrderContent>
    {
        public Task<OrderContent[]> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
