using Domain.Enums.Product;

namespace Application.Common.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Domain.Entities.Product>
    {

        public Task<Domain.Entities.Product[]> GetAllWithImagesAsync(CancellationToken cancellationToken = default);

        public Task<Domain.Entities.Product[]> GetByProductTypeWithImagesAsync(ProductType productType, CancellationToken cancellationToken = default);

        public Task<Domain.Entities.Product?> GetByProductCodeAsync(string productCode, CancellationToken cancellationToken = default);

        public Task<Domain.Entities.Product[]> GetByProductsIdsAsync(Guid[] productsId, CancellationToken cancellationToken = default);

    }
}
