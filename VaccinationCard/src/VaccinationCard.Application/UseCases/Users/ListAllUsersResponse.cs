namespace VaccinationCard.Application.UseCases.Users;

public record ListAllUsersResponse
{
    public Guid UserId { get; init; }
    public string Email { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public bool IsActive { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }

    public PatientInfoDto? Patient { get; init; }
}

public record PatientInfoDto
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string IdentificationNumber { get; init; } = string.Empty;
}