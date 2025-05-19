using Application.Product.Queries.GetProduct;

namespace Application.User.Queries.GetUserProducts
{
    public record GetUserProductsDto
    {
        public required GetProductDto[] Products { get; init; }

    }
}
