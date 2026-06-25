using MediatR;

namespace VaccinationCard.Application.UseCases.VaccinationRegisters.List;

public class PatientVaccinationCardCommand : IRequest<PatientVaccinationCardResponse>
{
    public Guid PatientId { get; set; }
}
