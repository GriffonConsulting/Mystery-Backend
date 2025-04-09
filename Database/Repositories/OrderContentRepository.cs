using Application.Common.Interfaces.Repositories;
using Database.Commands;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    public class OrderContentRepository : BaseRepository<OrderContent>, IOrderContentRepository
    {
        public OrderContentRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<OrderContent[]> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<OrderContent>().Include(c => c.Product).Include(c => c.Product.ProductImage).Where(i => i.UserId == userId).ToArrayAsync(cancellationToken: cancellationToken);
        }
    }
}
