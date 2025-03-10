using Domain.Enums.Product;
using System.ComponentModel.DataAnnotations;

namespace Application.Product.Queries.GetProduct
{
    public record GetProductDto
    {
        [Required]
        public required Guid Id { get; init; }
        [Required]
        public required string ProductCode { get; init; }
        [Required]
        public required string Title { get; init; }
        [Required]
        public required string Subtitle { get; init; }
        [Required]
        public required string Description { get; init; }
        [Required]
        public int NbPlayerMin { get; init; }
        [Required]
        public int NbPlayerMax { get; init; }
        [Required]
        public decimal Price { get; init; }
        [Required]
        public required string Duration { get; init; }
        [Required]
        public required List<string> Images { get; init; }
        [Required]
        public Difficulty Difficulty { get; init; }
        [Required]
        public ProductType ProductType { get; init; }

    }
}
