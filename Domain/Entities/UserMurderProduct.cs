using Domain.Common;

namespace Domain.Entities
{

    public class UserMurderProduct : IAuditableEntity
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderContentId { get; set; }
        public int NbPlayers { get; set; }
        public virtual User? User { get; set; }
        public virtual Product? Product { get; set; }
        public virtual OrderContent? OrderContent { get; set; }

    }
}
