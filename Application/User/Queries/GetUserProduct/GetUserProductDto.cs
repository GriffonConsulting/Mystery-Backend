using Application.Product.Queries.GetProduct;
using Domain.Enums.Product;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace Application.User.Queries.GetUserProduct
{
    public record GetUserProductDto
    {
        public ProductType ProductType { get; init; }
        public required JObject ProductUserConfiguration { get; init; }
        public required string ProductCode { get; init; }
        public required string Title { get; init; }
        public required string Subtitle { get; init; }
        public required string Description { get; init; }
        public int NbPlayerMin { get; init; }
        public int NbPlayerMax { get; init; }
        public decimal Price { get; init; }
        public required string Duration { get; init; }
        public Difficulty Difficulty { get; init; }
    }
}
