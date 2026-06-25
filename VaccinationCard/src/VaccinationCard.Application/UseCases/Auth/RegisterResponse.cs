
namespace VaccinationCard.Application.UseCases.Auth;

public record RegisterResponse
{
    public Guid UserId { get; init; }
    public string Email { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public string AccessToken { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
}
