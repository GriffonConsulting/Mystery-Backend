using Domain.Entities;
using EntityFramework;

namespace Database.Queries
{
    public class PromoCodeQueries : DbQueriesBase<PromoCode>
    {
        public PromoCodeQueries(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
