namespace Application.Authentication.Commands.ForgotPassword
{
    public record ForgotPasswordDto
    {
        public required string Token { get; init; }
        public DateTime ExpirationDate { get; init; }
    }
}
