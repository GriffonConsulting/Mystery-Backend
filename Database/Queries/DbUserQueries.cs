using Domain.Entities;
using EntityFramework;

namespace Database.Queries
{
    public class DbUserQueries : DbQueriesBase<User>
    {
        public DbUserQueries(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
