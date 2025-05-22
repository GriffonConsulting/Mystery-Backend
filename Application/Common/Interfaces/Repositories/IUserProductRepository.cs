using Domain.Entities;

namespace Application.Common.Interfaces.Repositories
{
    public interface IUserProductRepository : IRepository<Domain.Entities.UserProduct>
    {
        public Task<UserProduct?> GetByIdWithProductAsync(Guid userProductId, CancellationToken cancellationToken = default);
    }
}
