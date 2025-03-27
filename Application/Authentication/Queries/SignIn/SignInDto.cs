namespace Application.Authentication.Queries.SignIn
{
    public record SignInDto
    {
        public required string Token { get; init; }
        public DateTime ExpirationDate { get; init; }
    }
}
