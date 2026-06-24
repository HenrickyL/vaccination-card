namespace VaccinationCard.Domain.Common;

public abstract class BaseEntity : IEntity, IAuditableEntity, ISoftDeletableEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();

    // Audit fields
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }

    // Soft delete fields
    public bool IsDeleted { get; protected set; }
    public DateTime? DeletedAt { get; protected set; }

    protected BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
    }

    // ------
    public void UpdateTimestamp()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public void SoftDelete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        UpdateTimestamp();
    }

    public void Restore()
    {
        IsDeleted = false;
        DeletedAt = null;
        UpdateTimestamp();
    }
}
