using Domain.Entities;
using EntityFramework;

namespace Database.Queries
{
    public class UserProductQueries : DbQueriesBase<UserProduct>
    {
        public UserProductQueries(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
