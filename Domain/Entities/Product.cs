using Domain.Common;
using Domain.Enums.Product;

namespace Domain.Entities
{
    public class Product : IAuditableEntity
    {
        public required string ProductCode { get; set; }
        public required string Title { get; set; }
        public required string Subtitle { get; set; }
        public required string Description { get; set; }
        public int NbPlayerMin { get; set; }
        public int NbPlayerMax { get; set; }
        public decimal Price { get; set; }
        public required string PriceCode { get; set; }
        public required string Duration { get; set; }
        public Difficulty Difficulty { get; set; }
        public ProductType ProductType { get; set; }
        public required virtual List<ProductImage> ProductImage { get; set; }

    }
}
