using MediatR;

namespace VaccinationCard.Application.UseCases.Vaccines.Delete;

public class DeleteVaccineCommand : IRequest
{
    public Guid Id { get; init; }
}
