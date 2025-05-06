using Application.Common.Interfaces.Repositories;
using Domain.Entities;

namespace Database.Repositories
{
    public class UserProductRepository : BaseRepository<UserMurderProduct>, IUserProductRepository
    {
        public UserProductRepository(AppDbContext dbContext) : base(dbContext) { }


    }
}
