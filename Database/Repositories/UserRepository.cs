using Domain.Entities;
using Application.Common.Interfaces.Repositories;

namespace Database.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext) { }


    }
}
