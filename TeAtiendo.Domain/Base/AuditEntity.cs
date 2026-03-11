namespace TeAtiendo.Domain.Base
{
    public abstract class AuditEntity
    {
        public bool Activo { get; set; } = true;

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifyDate { get; set; }

        public Guid CreationUser { get; set; }
        public Guid? UserMod { get; set; }

        public Guid? UserDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public void SoftDelete(Guid userId)
        {
            Activo = false;
            UserDeleted = userId;
            DeletedDate = DateTime.UtcNow;
        }

        public void MarkModified(Guid userId)
        {
            UserMod = userId;
            ModifyDate = DateTime.UtcNow;
        }
    }
}