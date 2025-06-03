using Application.Common.Requests;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Invoices.Queries.GetInvoice;

public class GetInvoiceQuery : IRequest<RequestResult<GetInvoiceDto>>
{
    public Guid OrderId { get; set; }
}
