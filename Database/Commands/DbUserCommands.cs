using Domain.Entities;
using EntityFramework;

namespace Database.Commands
{
    public class DbUserCommands : DbCommandsBase<User>
    {
        public DbUserCommands(AppDbContext dbContext) : base(dbContext) { }


    }
}
