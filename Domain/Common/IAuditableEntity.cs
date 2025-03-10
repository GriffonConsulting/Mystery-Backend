namespace Domain.Common
{

    public abstract class IAuditableEntity : IBaseEntity
    {
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}