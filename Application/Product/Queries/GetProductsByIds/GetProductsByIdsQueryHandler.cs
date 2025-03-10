using Application.Common.Exceptions;
using Application.Common.Requests;
using Application.Product.Queries.GetProduct;
using Database.Queries;
using MediatR;

namespace Application.Product.Queries.GetProductsByIds
{
    public class GetProductsByIdsQueryHandler : IRequestHandler<GetProductsByIdsQuery, RequestResult<GetProductDto[]>>
    {
        private DbProductQueries _productQueries { get; }

        public GetProductsByIdsQueryHandler(DbProductQueries productQueries)
        {
            _productQueries = productQueries;
        }


        public async Task<RequestResult<GetProductDto[]>> Handle(GetProductsByIdsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productQueries.GetByProductsIdsAsync(request.ProductsIds, cancellationToken);
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
