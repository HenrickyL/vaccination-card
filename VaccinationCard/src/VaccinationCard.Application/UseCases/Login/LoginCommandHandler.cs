using MediatR;
using VaccinationCard.Domain.Enums;
using VaccinationCard.Domain.Exceptions;
using VaccinationCard.Domain.Interfaces;
using VaccinationCard.Domain.Repositories;

namespace VaccinationCard.Application.UseCases.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        // 1. Buscar usuário por email
        var user = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (user == null)
            throw new BadRequestException("Invalid credentials", ErrorCode.InvalidCredentials);

        // 2. Verificar senha
        if (!_passwordHasher.Verify(user.PasswordHash, command.Password))
            throw new BadRequestException("Invalid credentials", ErrorCode.InvalidCredentials);

        // 3. Verificar se usuário está ativo
        if (!user.IsActive)
            throw new ForbiddenException("User is inactive");
        // 4. Gerar token JWT
        var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role);

        return new LoginResponse
        {
            UserId = user.Id,
            Email = user.Email,
            AccessToken = token,
            Role = user.Role.ToString()
        };
    }
}
