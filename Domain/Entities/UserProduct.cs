using Domain.Common;

namespace Domain.Entities
{

    public class UserProduct : IAuditableEntity
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
