using Domain.Common;

namespace Domain.Entities
{
    public class PromoCode : IAuditableEntity
    {
        public required string CodeName { get; set; }
        public Guid ProductId { get; set; }
        public bool IsActive { get; set; }
    }
}
