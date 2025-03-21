using Domain.Entities;
using Stripe.Checkout;

namespace Application.Common.Interfaces.Services
{
    public interface IPaymentService
    {
        public Session CreateSession(string email, Guid userId, Domain.Entities.Product[] products, Guid[] productsIds);
    }
}