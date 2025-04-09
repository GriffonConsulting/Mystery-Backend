namespace Application.User.Queries.GetUser
{
    public record GetUserDto
    {
        public required string Email { get; init; }
        public string? Firstname { get; init; }
        public string? Lastname { get; init; }
        public string? Address { get; init; }
        public string? ComplementaryAddress { get; init; }
        public string? PostalCode { get; init; }
        public string? City { get; init; }
        public string? Country { get; init; }
        public bool MarketingEmail { get; init; }

    }
}
