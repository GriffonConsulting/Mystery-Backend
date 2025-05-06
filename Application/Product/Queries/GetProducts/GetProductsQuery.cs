using Application.Common.Requests;
using Application.Product.Queries.GetProduct;
using Application.UserProduct.GetProduct;
using Domain.Enums.Product;
using MediatR;

namespace Application.Product.Queries.GetProducts;

public class GetProductsQuery : IRequest<RequestResult<GetProductDto[]>>
{
    public ProductType ProductType { get; set; }
}
