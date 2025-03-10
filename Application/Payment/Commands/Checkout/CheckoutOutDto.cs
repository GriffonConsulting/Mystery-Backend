namespace Application.Payment.Commands.Checkout
{
    public record CheckoutOutDto
    {
        public required string ClientSecret { get; init; }
    }
}
