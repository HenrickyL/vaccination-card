using MediatR;

namespace VaccinationCard.Application.UseCases.Login;

public class LoginCommand : IRequest<LoginResponse>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
