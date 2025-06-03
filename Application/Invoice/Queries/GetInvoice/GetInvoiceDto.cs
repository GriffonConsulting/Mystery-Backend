namespace Application.Invoices.Queries.GetInvoice
{
    public record GetInvoiceDto
    {
        public required string InvoiceUrl { get; init; }
    }
}
