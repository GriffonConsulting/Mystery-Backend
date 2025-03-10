using Domain.Entities;
using Stripe.Checkout;

namespace Payment
{
    public interface IPaymentService
    {
        public Session CreateSession(string email, Guid userId, Product[] products, Guid[] productsIds);
    }
}