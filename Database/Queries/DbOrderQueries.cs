using Domain.Entities;
using EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Database.Queries
{
    public class DbOrderQueries : DbQueriesBase<Order>
    {
        public DbOrderQueries(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Order[]> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return DbContext.Set<Order>().Where(i => i.UserId == userId).ToArrayAsync(cancellationToken: cancellationToken);
        }

    }
}
