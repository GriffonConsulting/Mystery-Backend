using Application.Common.Requests;
using MediatR;

namespace Application.Payment.Commands.Checkout
{
    public class CheckoutCommand : IRequest<RequestResult<CheckoutOutDto>>
    {
        public required Guid UserId { get; set; }
        public required string Email { get; set; }
        public required CheckoutProductsCommand CheckoutProductsCommand { get; set; }
    }

    public class CheckoutProductsCommand
    {
        public required Guid[] ProductsIds { get; set; }

    }
}
