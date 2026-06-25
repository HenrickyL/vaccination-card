using MediatR;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Exceptions;
using VaccinationCard.Domain.Repositories;

namespace VaccinationCard.Application.UseCases.VaccinationRegisters.List;

public class PatientVaccinationCardCommandHandler : IRequestHandler<PatientVaccinationCardCommand, PatientVaccinationCardResponse>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IVaccineRegisterRepository _recordRepository;

    public PatientVaccinationCardCommandHandler(
        IPatientRepository patientRepository,
        IVaccineRegisterRepository recordRepository)
    {
        _patientRepository = patientRepository;
        _recordRepository = recordRepository;
    }

    public async Task<PatientVaccinationCardResponse> Handle(PatientVaccinationCardCommand query, CancellationToken cancellationToken)
    {
        // 1. Buscar paciente
        var patient = await _patientRepository.GetByIdAsync(query.PatientId, cancellationToken);
        if (patient == null)
            throw new NotFoundException("Patient not found");

        // 2. Buscar registros de vacinação
        var records = await _recordRepository.GetByPatientIdAsync(query.PatientId, cancellationToken);
        var recordsList = records.ToList();

        // 3. Construir resposta
        var response = new PatientVaccinationCardResponse
        {
            // Dados do Paciente
            Patient = new PatientInfoDto
            {
                Id = patient.Id,
                FullName = patient.FullName,
                IdentificationNumber = patient.IdentificationNumber,
                CreatedAt = patient.CreatedAt,
                UpdatedAt = patient.UpdatedAt
            },

            // Lista de Vacinações
            Registrations = recordsList.Select(r => new VaccinationRecordDto
            {
                Id = r.Id,
                VaccineId = r.VaccineId,
                VaccineName = r.Vaccine?.Name ?? string.Empty,
                VaccineCode = r.Vaccine?.Code ?? string.Empty,
                ApplicationDate = r.ApplicationDate,
                Dose = r.Dose,
                Lot = r.Lot,
                Observations = r.Observations,
                RegisteredAt = r.RegisteredAt,
                CreatedAt = r.CreatedAt
            }),

            // Resumo
            Summary = BuildSummary(recordsList)
        };

        return response;
    }

    private VaccinationSummaryDto BuildSummary(IEnumerable<VaccineRegister> records)
    {
        var recordsList = records.ToList();

        if (!recordsList.Any())
        {
            return new VaccinationSummaryDto
            {
                TotalVaccines = 0,
                TotalDoses = 0,
                LastVaccinationDate = null,
                FirstVaccinationDate = null,
                VaccinesByType = new Dictionary<string, int>()
            };
        }

        // Agrupar por vacina
        var vaccinesByType = recordsList
            .GroupBy(r => r.Vaccine?.Name ?? "Unknown")
            .ToDictionary(
                g => g.Key,
                g => g.Count()
            );

        return new VaccinationSummaryDto
        {
            TotalVaccines = recordsList.Select(r => r.VaccineId).Distinct().Count(),
            TotalDoses = recordsList.Count,
            LastVaccinationDate = recordsList.Max(r => r.ApplicationDate),
            FirstVaccinationDate = recordsList.Min(r => r.ApplicationDate),
            VaccinesByType = vaccinesByType
        };
    }
}
