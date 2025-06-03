using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using MediatR;
using Stripe;

namespace Application.Payment.Commands.InvoiceFinalized
{
    public class InvoicePaymentSucceededCommandHandler : IRequestHandler<InvoicePaymentSucceededCommand, RequestResult>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IFileStorage fileStorage;

        public InvoicePaymentSucceededCommandHandler(IOrderRepository orderRepository, IFileStorage fileStorage)
        {
            this.orderRepository = orderRepository;
            this.fileStorage = fileStorage;
        }


        public async Task<RequestResult> Handle(InvoicePaymentSucceededCommand request, CancellationToken cancellationToken)
        {
            var invoicePaymentService = new InvoicePaymentService();
            StripeList<InvoicePayment> invoicePayments = await invoicePaymentService.ListAsync(new InvoicePaymentListOptions { Invoice = request.Invoice.Id });
            var order = await orderRepository.GetByPaymentIntentIdAsync(invoicePayments.FirstOrDefault().Payment.PaymentIntentId, cancellationToken);

            if (order == null) throw new NotFoundException("orderNotFound");
            string invoiceUrl = $"invoices/{order.Id}.pdf";
            await fileStorage.UploadFileFromUrlAsync(request.Invoice.InvoicePdf, invoiceUrl);
            order.ReceiptUrl = invoiceUrl;
            await orderRepository.UpdateEntityAsync(order, cancellationToken);

            return new RequestResult
            {
            };
        }
    }
}
