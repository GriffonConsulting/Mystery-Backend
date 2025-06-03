using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using MediatR;

namespace Application.Invoices.Queries.GetInvoice
{
    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, RequestResult<GetInvoiceDto>>
    {
        private readonly IFileStorage fileStorage;

        private readonly IOrderRepository orderRepository;

        public GetInvoiceQueryHandler(IOrderRepository orderRepository, IFileStorage fileStorage)
        {
            this.orderRepository = orderRepository;
            this.fileStorage = fileStorage;
        }


        public async Task<RequestResult<GetInvoiceDto>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var invoice = await orderRepository.GetById(request.OrderId, cancellationToken);
            var invoiceUrl = await fileStorage.GetFileAsync(invoice.ReceiptUrl);

            return new RequestResult<GetInvoiceDto>
            {
                Result =  new GetInvoiceDto
                {
                    InvoiceUrl = invoiceUrl
                }
            };
        }
    }
}
