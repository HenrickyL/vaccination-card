using MediatR;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Exceptions;
using VaccinationCard.Domain.Interfaces;
using VaccinationCard.Domain.Repositories;


namespace VaccinationCard.Application.UseCases.Vaccines.Create;

public class CreateVaccineCommandHandler : IRequestHandler<CreateVaccineCommand, CreateVaccineResponse>
{
    private readonly IVaccineRepository _vaccineRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateVaccineCommandHandler(
        IVaccineRepository vaccineRepository,
        IUnitOfWork unitOfWork)
    {
        _vaccineRepository = vaccineRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateVaccineResponse> Handle(CreateVaccineCommand command, CancellationToken cancellationToken)
    {
        if (await _vaccineRepository.ExistsByCodeAsync(command.Code, cancellationToken))
            throw new ValidationException("Code","Vaccine with this name already exists");
        var vaccine = new Vaccine(command.Name, command.Code,command.Description);
        _vaccineRepository.Add(vaccine);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateVaccineResponse
        {
            Id = vaccine.Id,
            Name = vaccine.Name,
            CreatedAt = vaccine.CreatedAt
        };
    }
}
