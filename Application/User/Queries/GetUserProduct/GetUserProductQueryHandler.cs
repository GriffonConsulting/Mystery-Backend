using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using Application.Product.Queries.GetProduct;
using MediatR;

namespace Application.User.Queries.GetUserProduct;

public class GetUserProductQueryHandler : IRequestHandler<GetUserProductQuery, RequestResult<GetUserProductDto>>
{
    private IOrderContentRepository _orderContentRepository { get; }

    public GetUserProductQueryHandler(IOrderContentRepository orderContentRepository)
    {
        _orderContentRepository = orderContentRepository;
    }

    public async Task<RequestResult<GetUserProductDto>> Handle(GetUserProductQuery request, CancellationToken cancellationToken)
    {
        //var orderContents = await _orderContentRepository.GetByUserIdAsync(request.ClientId, cancellationToken);

        //return new RequestResult<GetUserProductDto>
        //{
        //    Result = new GetUserProductDto
        //    {
        //    }
        //};
        throw new NotImplementedException();
    }
}