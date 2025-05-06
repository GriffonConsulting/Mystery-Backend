using Domain.Common;

namespace Domain.Entities
{

    public class UserMurderProductPlayer : IAuditableEntity
    {
        public Guid UserProductId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public virtual UserMurderProduct? UserProduct { get; set; }
    }
}
