using Application.Common.Interfaces.Repositories;
using Database.Commands;
using Domain.Entities;
using EntityFramework;

namespace Database.Repositories
{
    public class OrderContentRepository : BaseRepository<OrderContent>, IOrderContentRepository
    {
        public OrderContentRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
