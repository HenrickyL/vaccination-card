using MediatR;
using VaccinationCard.Domain.Exceptions;
using VaccinationCard.Domain.Interfaces;
using VaccinationCard.Domain.Repositories;

namespace VaccinationCard.Application.UseCases.Vaccines.Delete;

public class DeleteVaccineCommandHandler : IRequestHandler<DeleteVaccineCommand>
{
    private readonly IVaccineRepository _vaccineRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVaccineCommandHandler(
        IVaccineRepository recordRepository,
        IUnitOfWork unitOfWork)
    {
        _vaccineRepository = recordRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteVaccineCommand command, CancellationToken cancellationToken)
    {
        var record = await _vaccineRepository.GetByIdAsync(command.Id, cancellationToken);
        if (record == null)
            throw new NotFoundException("Vaccination record not found");

        _vaccineRepository.Delete(record);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
