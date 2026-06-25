using MediatR;

namespace VaccinationCard.Application.UseCases.Users;

public class ListAllUsersCommand : IRequest<IEnumerable<ListAllUsersResponse>>
{
}
