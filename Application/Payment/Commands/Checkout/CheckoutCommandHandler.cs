using Application.Common.Interfaces;
using Application.Common.Requests;
using Database.Queries;
using MediatR;
using Stripe.Checkout;

namespace Application.Payment.Commands.Checkout
{
    public class CheckoutCommandHandler : IRequestHandler<CheckoutCommand, RequestResult<CheckoutOutDto>>
    {
        private readonly DbProductQueries _productQueries;
        private readonly IPaymentService _paymentService;

        public CheckoutCommandHandler(DbProductQueries productQueries, IPaymentService paymentService)
        {
            _productQueries = productQueries;
            _paymentService = paymentService;
        }

        public async Task<RequestResult<CheckoutOutDto>> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            var products = await _productQueries.GetByProductsIdsAsync(request.CheckoutProductsCommand.ProductsIds, cancellationToken);

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
