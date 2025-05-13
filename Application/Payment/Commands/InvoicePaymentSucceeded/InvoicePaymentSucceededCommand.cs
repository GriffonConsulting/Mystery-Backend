using Application.Common.Requests;
using MediatR;
using Stripe;

namespace Application.Payment.Commands.InvoiceFinalized
{
    public class InvoicePaymentSucceededCommand : IRequest<RequestResult>
    {
        public required Invoice Invoice { get; set; }
    }
}
