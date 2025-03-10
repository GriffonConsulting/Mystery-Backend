using Domain.Common;

namespace Domain.Entities
{
    public class Order : IAuditableEntity
    {
        public required string StripeId { get; set; }
        public required string ReceiptUrl { get; set; }
        public required decimal Amount { get; set; }
        public required Guid UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
