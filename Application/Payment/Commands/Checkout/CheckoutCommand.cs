using Application.Common.Requests;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Payment.Commands.Checkout
{
    public class CheckoutCommand : IRequest<RequestResult<CheckoutOutDto>>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public string? Email { get; set; }
        public required string ReturnUrl { get; set; }
        public required Guid[] ProductsIds { get; set; }
    }
}
