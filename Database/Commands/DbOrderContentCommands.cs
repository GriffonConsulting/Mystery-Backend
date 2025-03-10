using Domain.Entities;
using EntityFramework;

namespace Database.Commands
{
    public class OrderContentCommands : DbCommandsBase<OrderContent>
    {
        public OrderContentCommands(AppDbContext dbContext) : base(dbContext) { }


    }
}
