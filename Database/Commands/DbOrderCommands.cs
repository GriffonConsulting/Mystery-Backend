using Domain.Entities;
using EntityFramework;

namespace Database.Commands
{
    public class DbOrderCommands : DbCommandsBase<Order>
    {
        public DbOrderCommands(AppDbContext dbContext) : base(dbContext) { }


    }
}
