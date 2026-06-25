
namespace VaccinationCard.Application.UseCases.Login;

public record LoginResponse
{
    public Guid UserId { get; init; }
    public string Email { get; init; } = string.Empty;
    public string AccessToken { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
}
