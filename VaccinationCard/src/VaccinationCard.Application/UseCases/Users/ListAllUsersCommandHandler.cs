using MediatR;
using VaccinationCard.Domain.Repositories;

namespace VaccinationCard.Application.UseCases.Users;

public class ListAllUsersCommandHandler : IRequestHandler<ListAllUsersCommand, IEnumerable<ListAllUsersResponse>>
{
    private readonly IUserRepository _userRepository;

    public ListAllUsersCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<ListAllUsersResponse>> Handle(ListAllUsersCommand query, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllWithPatientAsync(cancellationToken);

        return users.Select(user => new ListAllUsersResponse
        {
            UserId = user.Id,
            Email = user.Email,
            Role = user.Role.ToString(),
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            Patient = user.PatientId.HasValue && user.Patient != null ? new PatientInfoDto
            {
                Id = user.Patient.Id,
                FullName = user.Patient.FullName,
                IdentificationNumber = user.Patient.IdentificationNumber,
            } : null
        });
    }
}
