namespace Application.Authentication.Commands.SignIn
{
    public record SignInDto
    {
        public required string Token { get; init; }
        public DateTime ExpirationDate { get; init; }
    }
}
