using Domain.Entities;
using EntityFramework;

namespace Database.Queries
{
    public class UserQueries : DbQueriesBase<User>
    {
        public UserQueries(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
