using Domain.Entities;
using Domain.Enums.Product;
using EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Database.Queries
{
    public class DbProductQueries : DbQueriesBase<Product>
    {
        public DbProductQueries(AppDbContext dbContext) : base(dbContext)
        {
        }
        public Task<Product[]> GetAllWithImagesAsync(CancellationToken cancellationToken = default)
        {
            return DbContext.Set<Product>().Include(c => c.ProductImage).ToArrayAsync(cancellationToken: cancellationToken);
        }

        public Task<Product[]> GetByProductTypeWithImagesAsync(ProductType productType, CancellationToken cancellationToken = default)
        {
            return DbContext.Set<Product>().Include(c => c.ProductImage).Where(i => i.ProductType == productType).ToArrayAsync(cancellationToken: cancellationToken);
        }

        public Task<Product?> GetByProductCodeAsync(string productCode, CancellationToken cancellationToken = default)
        {
            return DbContext.Set<Product>().Include(c => c.ProductImage).Where(i => i.ProductCode == productCode).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public Task<Product[]> GetByProductsIdsAsync(Guid[] productsId, CancellationToken cancellationToken = default)
        {
            return DbContext.Set<Product>().Include(c => c.ProductImage).Where(i => productsId.Contains(i.Id)).ToArrayAsync(cancellationToken: cancellationToken);
        }
    }
}
