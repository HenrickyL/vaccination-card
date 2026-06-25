using MediatR;

namespace VaccinationCard.Application.UseCases.Vaccines.Create;

public class CreateVaccineCommand : IRequest<CreateVaccineResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Code { get; set; }
}
