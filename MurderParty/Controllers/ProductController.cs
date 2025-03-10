using Application.Common.Requests;
using Application.Product.Queries.GetProduct;
using Application.Product.Queries.GetProducts;
using Application.Product.Queries.GetProductsByIds;
using Domain.Enums.Product;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MurderParty.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{productCode}", Name = "GetProduct")]
        [ProducesResponseType(typeof(RequestResult<GetProductDto>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetProduct([FromRoute] string productCode, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProductQuery { ProductCode = productCode }, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{productType}/All", Name = "GetProducts")]
        [ProducesResponseType(typeof(RequestResult<GetProductDto[]>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts([FromRoute] ProductType productType, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProductsQuery { ProductType = productType }, cancellationToken);
            return Ok(result);
        }

        [HttpPost("ByIds", Name = "GetProductsByIds")]
        [ProducesResponseType(typeof(RequestResult<GetProductDto[]>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsByIds([FromBody] Guid[] productsIds, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProductsByIdsQuery { ProductsIds = productsIds }, cancellationToken);
            return Ok(result);
        }

    }
}