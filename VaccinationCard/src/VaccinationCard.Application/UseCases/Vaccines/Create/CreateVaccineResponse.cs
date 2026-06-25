namespace VaccinationCard.Application.UseCases.Vaccines.Create;

public record CreateVaccineResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}
