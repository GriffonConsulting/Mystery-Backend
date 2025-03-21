using Application.Common.Interfaces.Repositories;
using Database.Commands;
using Domain.Entities;
using EntityFramework;

namespace Database.Repositories
{
    public class UserProductRepository : BaseRepository<UserProduct>, IUserProductRepository
    {
        public UserProductRepository(AppDbContext dbContext) : base(dbContext) { }


    }
}
