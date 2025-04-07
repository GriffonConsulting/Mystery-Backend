using Application.Common.Interfaces.Repositories;
using Database.Commands;
using Domain.Entities;
using Domain.Enums.Product;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<Product[]> GetAllWithImagesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<Product>().Include(c => c.ProductImage).ToArrayAsync(cancellationToken: cancellationToken);
        }

        public Task<Product[]> GetByProductTypeWithImagesAsync(ProductType productType, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<Product>().Include(c => c.ProductImage).Where(i => i.ProductType == productType).ToArrayAsync(cancellationToken: cancellationToken);
        }

        public Task<Product?> GetByProductCodeAsync(string productCode, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<Product>().Include(c => c.ProductImage).Where(i => i.ProductCode == productCode).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public Task<Product[]> GetByProductsIdsAsync(Guid[] productsId, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<Product>().Include(c => c.ProductImage).Where(i => productsId.Contains(i.Id)).ToArrayAsync(cancellationToken: cancellationToken);
        }

    }
}
