using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using Application.Product.Queries.GetProduct;
using MediatR;

namespace Application.User.Queries.GetUserGames;

public class GetUserGamesQueryHandler : IRequestHandler<GetUserGamesQuery, RequestResult<GetUserGamesDto>>
{
    private IOrderContentRepository _orderContentRepository { get; }

    public GetUserGamesQueryHandler(IOrderContentRepository orderContentRepository)
    {
        _orderContentRepository = orderContentRepository;
    }

    public async Task<RequestResult<GetUserGamesDto>> Handle(GetUserGamesQuery request, CancellationToken cancellationToken)
    {
        var orderContents = await _orderContentRepository.GetByUserIdAsync(request.ClientId, cancellationToken);

        return new RequestResult<GetUserGamesDto>
        {
            Result = new GetUserGamesDto
            {
                Products = orderContents.Select(orderContent => new GetProductDto
                {
                    Id = orderContent.Id,
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