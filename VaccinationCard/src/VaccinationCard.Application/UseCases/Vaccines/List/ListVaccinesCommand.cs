using MediatR;
using VaccinationCard.Application.UseCases.Vaccines.List;

namespace VaccinationCard.Application.UseCases.Vaccines.Lis;

public class ListVaccinesCommand : IRequest<IEnumerable<ListVaccinesResponse>>
{

}
