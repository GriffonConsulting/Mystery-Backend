using Application.Common.Interfaces.Repositories;
using Domain.Entities;

namespace Database.Repositories
{
    public class UserProductRepository : BaseRepository<UserProduct>, IUserProductRepository
    {
        public UserProductRepository(AppDbContext dbContext) : base(dbContext) { }


    }
}
