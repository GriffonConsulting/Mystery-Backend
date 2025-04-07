using Application.Common.Interfaces.Repositories;
using Database.Commands;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    public class FaqRepository : BaseRepository<Faq>, IFaqRepository
    {
        public FaqRepository(AppDbContext dbContext) : base(dbContext) { }


        public Task<Faq[]> GetByLangAsync(string language, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<Faq>().Where(f => f.Language == language).ToArrayAsync(cancellationToken: cancellationToken);
        }
    }
}
