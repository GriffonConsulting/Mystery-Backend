using Domain.Entities;

namespace Application.Common.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task<Order[]> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
