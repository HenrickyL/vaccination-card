using MediatR;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Enums;
using VaccinationCard.Domain.Exceptions;
using VaccinationCard.Domain.Interfaces;
using VaccinationCard.Domain.Repositories;

namespace VaccinationCard.Application.UseCases.Auth;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(
       IPatientRepository patientRepository,
       IUserRepository userRepository,
       IPasswordHasher passwordHasher,
       IJwtService jwtService,
       IUnitOfWork unitOfWork)
    {
        _patientRepository = patientRepository;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _unitOfWork = unitOfWork;
    }
    /// TODO: Use Seed to create Admin and validate token to register a EEmployee.
    public async Task<RegisterResponse> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // 1. Validar se email já existe
        if (await _userRepository.ExistsByEmailAsync(command.Email, cancellationToken))
            throw new ValidationException(
                "Email", 
                "Email already registered", 
                ErrorCode.EmailAlreadyExist);

        // 2. Validar se documento já existe
        if (await _patientRepository.ExistsByIdentificationNumberAsync(command.IdentificationNumber, cancellationToken))
            throw new ValidationException(
                "IdentificationNumber",
                "Identification number already registered",
                ErrorCode.IdentificationNumberAlreadyExist);

        // 3. Criar paciente
        var patient = new Patient(command.FullName, command.IdentificationNumber);
        _patientRepository.Add(patient);

        // 4. Criar usuário
        var passwordHash = _passwordHasher.Hash(command.Password);
        UserRole userRole = Enum.Parse<UserRole>(command.Role, ignoreCase: true);
        var user = new User(command.Email, passwordHash, userRole);
        user.LinkToPatient(patient.Id);
        //patient.LinkToUser(user.Id);

        _userRepository.Add(user);

        // 5. Salvar no banco
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 6. Gerar token JWT
        var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role);

        return new RegisterResponse
        {
            UserId = user.Id,
            Email = user.Email,
            FullName = patient.FullName,
            AccessToken = token,
            Role = user.Role.ToString()
        };
    }

}
