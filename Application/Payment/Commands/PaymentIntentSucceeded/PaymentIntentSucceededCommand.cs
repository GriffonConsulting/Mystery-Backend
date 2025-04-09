using Application.Common.Requests;
using MediatR;
using Stripe;

namespace Application.Payment.Commands.PaymentIntentSucceeded
{
    public class PaymentIntentSucceededCommand : IRequest<RequestResult>
    {
        public required PaymentIntent PaymentIntent { get; set; }
    }
}
