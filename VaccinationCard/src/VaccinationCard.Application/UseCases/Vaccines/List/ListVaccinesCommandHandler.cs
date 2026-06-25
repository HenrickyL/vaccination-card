using MediatR;
using VaccinationCard.Application.UseCases.Vaccines.Lis;
using VaccinationCard.Domain.Repositories;

namespace VaccinationCard.Application.UseCases.Vaccines.List;

public class ListVaccinesCommandHandler : IRequestHandler<ListVaccinesCommand, IEnumerable<ListVaccinesResponse>>
{
    private readonly IVaccineRepository _vaccineRepository;

    public ListVaccinesCommandHandler(IVaccineRepository vaccineRepository)
    {
        _vaccineRepository = vaccineRepository;
    }

    public async Task<IEnumerable<ListVaccinesResponse>> Handle(ListVaccinesCommand query, CancellationToken cancellationToken)
    {
        var vaccines = await _vaccineRepository.GetAllAsync(cancellationToken);

        return vaccines.Select(v => new ListVaccinesResponse
        {
            Id = v.Id,
            Name = v.Name,
            Code = v.Code,
            Description = v.Description,
            CreatedAt = v.CreatedAt
        });
    }
}
