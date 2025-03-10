using Application.Common.Requests;
using MediatR;

namespace Application.Invoices.Queries.GetInvoices;

public class GetInvoicesQuery : IRequest<RequestResult<GetInvoicesResult[]>>
{
    public Guid UserId { get; set; }
}
