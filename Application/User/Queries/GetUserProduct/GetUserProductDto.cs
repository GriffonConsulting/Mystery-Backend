using Domain.Enums.Product;
using Newtonsoft.Json.Linq;

namespace Application.User.Queries.GetUserProduct
{
    public record GetUserProductDto
    {
        public ProductType ProductType { get; set; }
        public required JObject ProductUserConfiguration { get; set; }
    }
}
