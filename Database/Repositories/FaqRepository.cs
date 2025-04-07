using Application.Common.Interfaces.Repositories;
using Database.Commands;
using Domain.Entities;

namespace Database.Repositories
{
    public class FaqRepository : BaseRepository<Faq>, IFaqRepository
    {
        public FaqRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
