using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using MediatR;
using Stripe;

namespace Application.Payment.Commands.InvoiceFinalized
{
    public class InvoicePaymentSucceededCommandHandler : IRequestHandler<InvoicePaymentSucceededCommand, RequestResult>
    {
        private IOrderRepository _orderRepository { get; }

        public InvoicePaymentSucceededCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public async Task<RequestResult> Handle(InvoicePaymentSucceededCommand request, CancellationToken cancellationToken)
        {
            var invoicePaymentService = new InvoicePaymentService();
            StripeList<InvoicePayment> invoicePayments = await invoicePaymentService.ListAsync(new InvoicePaymentListOptions { Invoice = request.Invoice.Id });
            var order = await _orderRepository.GetByPaymentIntentIdAsync(invoicePayments.FirstOrDefault().Payment.PaymentIntentId, cancellationToken);

            if (order == null) throw new NotFoundException("orderNotFound");

            order.ReceiptUrl = request.Invoice.InvoicePdf;
            await _orderRepository.UpdateEntityAsync(order, cancellationToken);

            return new RequestResult    
            {
            };
        }
    }
}
