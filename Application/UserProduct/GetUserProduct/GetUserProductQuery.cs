using Application.Common.Requests;
using MediatR;

namespace Application.UserProduct.GetProduct;

public class GetUserProductQuery : IRequest<RequestResult<GetUserProductDto>>
{
    public required string ProductCode { get; set; }
}
