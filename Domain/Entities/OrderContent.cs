using Domain.Common;

namespace Domain.Entities
{
    public class OrderContent : IAuditableEntity
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public Guid UserProductId { get; set; }
        public virtual Product? Product { get; set; }
        public virtual UserProduct? UserProduct { get; set; }
        public virtual User? User { get; set; }
        public virtual Order? Order { get; set; }

    }
}
