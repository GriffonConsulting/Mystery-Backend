using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using MediatR;
using Stripe.Checkout;

namespace Application.Payment.Commands.Checkout
{
    public class CheckoutCommandHandler : IRequestHandler<CheckoutCommand, RequestResult<CheckoutOutDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IPayment _paymentService;

        public CheckoutCommandHandler(IProductRepository productRepository, IPayment paymentService)
        {
            _productRepository = productRepository;
            _paymentService = paymentService;
        }

        public async Task<RequestResult<CheckoutOutDto>> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetByProductsIdsAsync(request.ProductsIds, cancellationToken);

            Session session = _paymentService.CreateSession(request.Email, request.ReturnUrl, request.UserId, products, request.ProductsIds);

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
