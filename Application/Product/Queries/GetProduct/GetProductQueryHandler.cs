using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using MediatR;

namespace Application.Product.Queries.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, RequestResult<GetProductDto>>
    {
        private IProductRepository _productQueries { get; }

        public GetProductQueryHandler(IProductRepository productRepository)
        {
            _productQueries = productRepository;
        }


        public async Task<RequestResult<GetProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productQueries.GetByProductCodeAsync(request.ProductCode, cancellationToken);
            if (product == null) throw new NotFoundException("product");

            return new RequestResult<GetProductDto>
            {
                Result = new GetProductDto
                {
                    Id = product.Id,
                    ProductCode = product.ProductCode,
                    Title = product.Title,
                    Subtitle = product.Subtitle,
                    Description = product.Description,
                    NbPlayerMin = product.NbPlayerMin,
                    NbPlayerMax = product.NbPlayerMax,
                    Price = product.Price,
                    Duration = product.Duration,
                    Difficulty = product.Difficulty,
                    ProductType = product.ProductType,
                    Images = product.ProductImage.Select(p => p.Link).ToList(),
                }
            };
        }
    }
}
