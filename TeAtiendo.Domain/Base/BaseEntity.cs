namespace TeAtiendo.Domain.Base
{
    public abstract class BaseEntity : AuditEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}