using Domain.Common;

namespace Domain.Entities
{
    public class Faq : IAuditableEntity
    {
        public required string Question { get; set; }
        public required string Answer { get; set; }
        public required string Language { get; set; }
    }
}
