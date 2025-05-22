using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using Application.Product.Queries.GetProduct;
using Domain.Entities;
using MediatR;
using Newtonsoft.Json.Linq;

namespace Application.User.Queries.GetUserProduct;

public class GetUserProductQueryHandler : IRequestHandler<GetUserProductQuery, RequestResult<GetUserProductDto>>
{
    private readonly IUserProductRepository userProductRepository;

    public GetUserProductQueryHandler(IUserProductRepository userProductRepository)
    {
        this.userProductRepository = userProductRepository;
    }

    public async Task<RequestResult<GetUserProductDto>> Handle(GetUserProductQuery request, CancellationToken cancellationToken)
    {
        var userProduct = await userProductRepository.GetByIdWithProductAsync(request.UserProductId, cancellationToken);

        return new RequestResult<GetUserProductDto>
        {
            Result = new GetUserProductDto
            {
                ProductType = userProduct.ProductType,
                ProductUserConfiguration = JObject.Parse(userProduct.ProductUserConfiguration),
                ProductCode = userProduct.Product.ProductCode,
                Title = userProduct.Product.Title,
                Subtitle = userProduct.Product.Subtitle,
                Description = userProduct.Product.Description,
                NbPlayerMin = userProduct.Product.NbPlayerMin,
                NbPlayerMax = userProduct.Product.NbPlayerMax,
                Price = userProduct.Product.Price,
                Duration = userProduct.Product.Duration,
                Difficulty = userProduct.Product.Difficulty,
            }
        };
    }
}