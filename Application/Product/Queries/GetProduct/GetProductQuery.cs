using Application.Common.Requests;
using MediatR;

namespace Application.Product.Queries.GetProduct;

public class GetProductQuery : IRequest<RequestResult<GetProductDto>>
{
    public required string ProductCode { get; set; }
}
