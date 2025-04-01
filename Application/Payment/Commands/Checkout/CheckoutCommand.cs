using Application.Common.Requests;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Payment.Commands.Checkout
{
    public class CheckoutCommand : IRequest<RequestResult<CheckoutOutDto>>
    {
        [JsonIgnore]
        public required Guid UserId { get; set; }
        [JsonIgnore]
        public required string Email { get; set; }
        public required CheckoutProductsCommand CheckoutProductsCommand { get; set; }
    }

    public class CheckoutProductsCommand
    {
        public required Guid[] ProductsIds { get; set; }

    }
}
