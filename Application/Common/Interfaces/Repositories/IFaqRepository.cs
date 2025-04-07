using Domain.Entities;

namespace Application.Common.Interfaces.Repositories
{
    public interface IFaqRepository : IRepository<Domain.Entities.Faq>
    {
        public Task<Domain.Entities.Faq[]> GetByLangAsync(string language, CancellationToken cancellationToken = default);
    }
}
