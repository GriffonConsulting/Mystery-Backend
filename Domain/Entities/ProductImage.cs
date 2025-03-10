using Domain.Common;

namespace Domain.Entities
{
    public class ProductImage : IAuditableEntity
    {
        public Guid ProductId { get; set; }
        public required string Link { get; set; }
        public virtual Product? Product { get; set; }
    }
}
