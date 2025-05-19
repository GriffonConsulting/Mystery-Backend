using Domain.Common;
using Domain.Enums.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{

    public class UserProduct : IAuditableEntity
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderContentId { get; set; }
        public ProductType ProductType { get; set; }
        [Column(TypeName = "jsonb")]
        public string? ProductUserConfiguration { get; set; }
        public virtual User? User { get; set; }
        public virtual Product? Product { get; set; }
        public virtual OrderContent? OrderContent { get; set; }

    }
}
