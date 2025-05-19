using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using Application.Product.Queries.GetProduct;
using Application.User.Queries.GetUserProduct;
using MediatR;

namespace Application.User.Queries.GetUserProducts;

public class GetUserProductsQueryHandler : IRequestHandler<GetUserProductsQuery, RequestResult<GetUserProductsDto>>
{
    private IOrderContentRepository _orderContentRepository { get; }

    public GetUserProductsQueryHandler(IOrderContentRepository orderContentRepository)
    {
        _orderContentRepository = orderContentRepository;
    }

    public async Task<RequestResult<GetUserProductsDto>> Handle(GetUserProductsQuery request, CancellationToken cancellationToken)
    {
        var orderContents = await _orderContentRepository.GetByUserIdAsync(request.ClientId, cancellationToken);

        return new RequestResult<GetUserProductsDto>
        {
            Result = new GetUserProductsDto
            {
                Products = orderContents.Select(orderContent => new GetProductDto
                {
                    Id = orderContent.UserProductId,
                    ProductCode = orderContent.Product.ProductCode,
                    Title = orderContent.Product.Title,
                    Subtitle = orderContent.Product.Subtitle,
                    Description = orderContent.Product.Description,
                    NbPlayerMin = orderContent.Product.NbPlayerMin,
                    NbPlayerMax = orderContent.Product.NbPlayerMax,
                    Price = orderContent.Product.Price,
                    Duration = orderContent.Product.Duration,
                    Difficulty = orderContent.Product.Difficulty,
                    ProductType = orderContent.Product.ProductType,
                    Images = orderContent.Product.ProductImage.Select(p => p.Link).ToList()
                }).ToArray()
            }
        };
    }
}