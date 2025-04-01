namespace Application.User.Queries.GetUser
{
    public record GetUserDto
    {
        public required string Email { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Address { get; set; }
        public string? ComplementaryAddress { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public bool MarketingEmail { get; set; }

    }
}
