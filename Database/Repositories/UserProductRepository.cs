using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    public class UserProductRepository : BaseRepository<UserProduct>, IUserProductRepository
    {
        public UserProductRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<UserProduct?> GetByIdWithProductAsync(Guid userProductId, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<UserProduct>().Include(c => c.Product).Where(up => up.Id == userProductId).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

    }
}
