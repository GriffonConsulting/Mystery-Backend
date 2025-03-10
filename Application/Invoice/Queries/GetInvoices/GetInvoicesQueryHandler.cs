using Application.Common.Exceptions;
using Application.Common.Requests;
using Database.Queries;
using MediatR;

namespace Application.Invoices.Queries.GetInvoices
{
    public class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, RequestResult<GetInvoicesResult[]>>
    {
        private DbOrderQueries _orderQueries { get; }

        public GetInvoicesQueryHandler(DbOrderQueries orderQueries)
        {
            _orderQueries = orderQueries;
        }


        public async Task<RequestResult<GetInvoicesResult[]>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _orderQueries.GetByUserIdAsync(request.UserId, cancellationToken);

            return new RequestResult<GetInvoicesResult[]>
            {
                Result = invoices.Select((i) => new GetInvoicesResult
                {
                    Amount = i.Amount,
                    CreatedOn = i.CreatedOn,
                    ReceiptUrl = i.ReceiptUrl,
                }).ToArray()
            };
        }
    }
}
