namespace Domain.Common
{

    public abstract class IAuditableEntity : IBaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}