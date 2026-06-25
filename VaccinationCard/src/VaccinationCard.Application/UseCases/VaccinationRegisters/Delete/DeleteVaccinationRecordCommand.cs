using MediatR;

namespace VaccinationCard.Application.UseCases.VaccinationRegisters.Delete;

public class DeleteVaccinationRecordCommand : IRequest
{
    public Guid Id { get; init; }
}
