namespace VaccinationCard.Application.UseCases.Vaccines.List;

public record ListVaccinesResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}
