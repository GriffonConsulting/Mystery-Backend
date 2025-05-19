using Application.Common.Requests;
using Application.Product.Queries.GetProduct;
using MediatR;

namespace Application.Product.Queries.GetProductsByIds;

public class GetProductsByIdsQuery : IRequest<RequestResult<GetProductDto[]>>
{
    public required Guid[] ProductsIds { get; set; }
}
