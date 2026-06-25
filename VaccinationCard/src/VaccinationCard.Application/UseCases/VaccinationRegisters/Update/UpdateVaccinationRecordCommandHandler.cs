using MediatR;
using VaccinationCard.Domain.Exceptions;
using VaccinationCard.Domain.Interfaces;
using VaccinationCard.Domain.Repositories;

namespace VaccinationCard.Application.UseCases.VaccinationRegisters.Update;

public class UpdateVaccinationRecordCommandHandler : IRequestHandler<UpdateVaccinationRecordCommand, UpdateVaccinationRecordResponse>
{
    private readonly IVaccineRegisterRepository _recordRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateVaccinationRecordCommandHandler(
        IVaccineRegisterRepository recordRepository,
        IUnitOfWork unitOfWork)
    {
        _recordRepository = recordRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<UpdateVaccinationRecordResponse> Handle(UpdateVaccinationRecordCommand command, CancellationToken cancellationToken)
    {
        var record = await _recordRepository.GetByIdAsync(command.Id, cancellationToken);
        if (record == null)
            throw new NotFoundException("Vaccination record not found");

        // Atualizar dados
        if (command.ApplicationDate != null)
            record.ApplicationDate = command.ApplicationDate.Value;
        if(command.Dose != null)
            record.Dose = command.Dose.Value;
        if(command.Lot != null)
            record.Lot = command.Lot;
        if(command.Observations != null)
            record.Observations = command.Observations;

        _recordRepository.Update(record);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateVaccinationRecordResponse
        {
            Id = record.Id,
            PatientId = record.PatientId,
            PatientName = record.Patient?.FullName ?? string.Empty,
            VaccineId = record.VaccineId,
            VaccineName = record.Vaccine?.Name ?? string.Empty,
            ApplicationDate = record.ApplicationDate,
            Dose = record.Dose,
            Lot = record.Lot,
            Observations = record.Observations,
            UpdatedAt = record.UpdatedAt
        };
    }
}
