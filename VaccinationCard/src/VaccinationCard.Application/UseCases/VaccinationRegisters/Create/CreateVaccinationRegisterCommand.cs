using MediatR;

namespace VaccinationCard.Application.UseCases.VaccinationRegisters.Create;
/// TODO: Rename all *Command to *Request
public class CreateVaccinationRegisterCommand : IRequest<CreateVaccinationRegisterResponse>
{
    public Guid PatientId { get; init; }
    public Guid VaccineId { get; init; }
    public DateTime ApplicationDate { get; init; }
    public int Dose { get; init; }
    public string? Lot { get; init; }
    public string? Observations { get; init; }
}
