using MediatR;

namespace VaccinationCard.Application.UseCases.Auth;

public class RegisterCommand : IRequest<RegisterResponse>
{
    public string FullName { get; init; } = string.Empty;
    public string IdentificationNumber { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string ConfirmPassword { get; init; } = string.Empty;
}
