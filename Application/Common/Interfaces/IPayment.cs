using Domain.Entities;
using Stripe.Checkout;

namespace Application.Common.Interfaces
{
    public interface IPayment
    {
        public Session CreateSession(string email, Guid userId, Domain.Entities.Product[] products, Guid[] productsIds);
    }
}