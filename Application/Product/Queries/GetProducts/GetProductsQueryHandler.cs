using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using Application.Product.Queries.GetProduct;
using MediatR;

namespace Application.Product.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, RequestResult<GetProductDto[]>>
    {
        private IProductRepository _productRepository    { get; }

        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<RequestResult<GetProductDto[]>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetByProductTypeWithImagesAsync(request.ProductType, cancellationToken);
            if (!products.Any()) throw new NotFoundException("products");

            return new RequestResult<GetProductDto[]>
            {
                Result = products.Select(product => new GetProductDto
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
                    Images = product.ProductImage.Select(p => p.Link).ToList()
                }).ToArray()
            };
        }
    }
}