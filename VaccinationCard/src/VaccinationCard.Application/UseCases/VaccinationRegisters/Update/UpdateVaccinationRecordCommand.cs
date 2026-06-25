
using MediatR;

namespace VaccinationCard.Application.UseCases.VaccinationRegisters.Update;

public class UpdateVaccinationRecordCommand : IRequest<UpdateVaccinationRecordResponse>
{
    public Guid Id { get; init; }
    public DateTime? ApplicationDate { get; init; }
    public int? Dose { get; init; }
    public string? Lot { get; init; }
    public string? Observations { get; init; }
}
