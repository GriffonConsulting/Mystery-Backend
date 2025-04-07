namespace Application.Faq.Queries.GetFaq
{
    public record GetFaqDto
    {
        public required string Question { get; init; }
        public required string Answer { get; init; }
    }
}
