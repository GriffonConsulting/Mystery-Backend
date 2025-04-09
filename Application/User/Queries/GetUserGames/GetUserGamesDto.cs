using Application.Product.Queries.GetProduct;

namespace Application.User.Queries.GetUserGames
{
    public record GetUserGamesDto
    {
        public required GetProductDto[] Products { get; init; }

    }
}
