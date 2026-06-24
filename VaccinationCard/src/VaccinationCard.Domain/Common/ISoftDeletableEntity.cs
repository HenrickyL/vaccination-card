namespace VaccinationCard.Domain.Common;

public interface ISoftDeletableEntity
{
    bool IsDeleted { get; }
    DateTime? DeletedAt { get; }
}
