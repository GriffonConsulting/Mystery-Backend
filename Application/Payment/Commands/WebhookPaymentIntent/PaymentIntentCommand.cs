using Application.Common.Requests;
using MediatR;
using Stripe;

namespace Application.Payment.Commands.WebhookPaymentIntent
{
    public class PaymentIntentCommand : IRequest<RequestResult>
    {
        public required PaymentIntent PaymentIntent { get; set; }
    }
}
