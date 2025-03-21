using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Common.Requests;
using MediatR;
using Stripe.Checkout;

namespace Application.Payment.Commands.Checkout
{
    public class CheckoutCommandHandler : IRequestHandler<CheckoutCommand, RequestResult<CheckoutOutDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IPaymentService _paymentService;

        public CheckoutCommandHandler(IProductRepository productRepository, IPaymentService paymentService)
        {
            _productRepository = productRepository;
            _paymentService = paymentService;
        }

        public async Task<RequestResult<CheckoutOutDto>> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetByProductsIdsAsync(request.CheckoutProductsCommand.ProductsIds, cancellationToken);

            Session session = _paymentService.CreateSession(request.Email, request.UserId, products, request.CheckoutProductsCommand.ProductsIds);

            return new RequestResult<CheckoutOutDto>
            {
                Result = new CheckoutOutDto
                {
                    ClientSecret = session.ClientSecret
                }


            };
        }

    }
}
