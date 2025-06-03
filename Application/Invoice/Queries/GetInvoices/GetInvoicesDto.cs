namespace Application.Invoices.Queries.GetInvoices
{
    public record GetInvoicesDto
    {
        public required Guid OrderId { get; init; }
        public required decimal Amount { get; init; }
        public required DateTime CreatedOn { get; init; }
    }
}
