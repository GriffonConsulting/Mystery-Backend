using Stripe.Checkout;

namespace Application.Common.Interfaces
{
    public interface IPayment
    {
        public Session CreateSession(string email, string returnUrl, Guid userId, Domain.Entities.Product[] products, Guid[] productsIds);
    }
}