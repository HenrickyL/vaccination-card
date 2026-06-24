namespace VaccinationCard.Domain.Common;

public class BaseEntity : IEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
}
