using MediatR;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Exceptions;
using VaccinationCard.Domain.Interfaces;
using VaccinationCard.Domain.Repositories;

namespace VaccinationCard.Application.UseCases.VaccinationRegisters.Create;

public class CreateVaccinationRegisterCommandHandler : IRequestHandler<CreateVaccinationRegisterCommand, CreateVaccinationRegisterResponse>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IVaccineRepository _vaccineRepository;
    private readonly IVaccineRegisterRepository _recordRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateVaccinationRegisterCommandHandler(
        IPatientRepository patientRepository,
        IVaccineRepository vaccineRepository,
        IVaccineRegisterRepository recordRepository,
        IUnitOfWork unitOfWork)
    {
        _patientRepository = patientRepository;
        _vaccineRepository = vaccineRepository;
        _recordRepository = recordRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateVaccinationRegisterResponse> Handle(CreateVaccinationRegisterCommand command, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(command.PatientId, cancellationToken);
        if (patient == null)
            throw new NotFoundException("Patient not found");

        //if (!patient.IsActive)
        //    throw new DomainException("Cannot register vaccination for inactive patient");

        var vaccine = await _vaccineRepository.GetByIdAsync(command.VaccineId, cancellationToken);
        if (vaccine == null)
            throw new NotFoundException("Vaccine not found");

        // Validate dose
        var existingRecords = await _recordRepository.GetByPatientAndVaccineAsync(
            command.PatientId,
            command.VaccineId,
            cancellationToken);

        if (existingRecords.Any(r => r.VaccineId == command.VaccineId &&  r.Dose == command.Dose))
            throw new BadRequestException($"Patient already received dose {command.Dose} of this vaccine");

        var record = new VaccineRegister(
            command.PatientId,
            command.VaccineId,
            command.ApplicationDate.ToUniversalTime(),
            command.Dose,
            command.Lot,
            command.Observations
        );

        _recordRepository.Add(record);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateVaccinationRegisterResponse
        {
            Id = record.Id,
            PatientId = patient.Id,
            PatientName = patient.FullName,
            PatientIdentification = patient.IdentificationNumber,
            VaccineId = vaccine.Id,
            VaccineName = vaccine.Name,
            VaccineCode = vaccine.Code,
            ApplicationDate = record.ApplicationDate,
            Dose = record.Dose,
            Lot = record.Lot,
            Observations = record.Observations,
            RegisteredAt = record.RegisteredAt,
            CreatedAt = record.CreatedAt
        };
    }
}
