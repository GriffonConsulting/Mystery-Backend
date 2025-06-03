namespace Application.Invoices.Queries.GetInvoices
{
    public record GetInvoicesDto
    {
        public required Guid Id { get; init; }
        public required decimal Amount { get; init; }
        public required DateTime CreatedOn { get; init; }
    }
}
