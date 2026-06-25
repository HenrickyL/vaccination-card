
using MediatR;
using VaccinationCard.Domain.Exceptions;
using VaccinationCard.Domain.Interfaces;
using VaccinationCard.Domain.Repositories;

namespace VaccinationCard.Application.UseCases.VaccinationRegisters.Delete;

public class DeleteVaccinationRecordCommandHandler : IRequestHandler<DeleteVaccinationRecordCommand>
{
    private readonly IVaccineRegisterRepository _recordRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVaccinationRecordCommandHandler(
        IVaccineRegisterRepository recordRepository,
        IUnitOfWork unitOfWork)
    {
        _recordRepository = recordRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteVaccinationRecordCommand command, CancellationToken cancellationToken)
    {
        var record = await _recordRepository.GetByIdAsync(command.Id, cancellationToken);
        if (record == null)
            throw new NotFoundException("Vaccination record not found");

        _recordRepository.Delete(record);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
