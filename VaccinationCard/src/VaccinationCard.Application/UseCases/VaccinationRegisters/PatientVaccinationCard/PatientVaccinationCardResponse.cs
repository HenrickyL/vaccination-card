namespace VaccinationCard.Application.UseCases.VaccinationRegisters.List;

public record PatientVaccinationCardResponse
{
    public PatientInfoDto Patient { get; init; } = new();
    public IEnumerable<VaccinationRecordDto> Registrations { get; init; } = new List<VaccinationRecordDto>();
    public VaccinationSummaryDto Summary { get; init; } = new();
}

public record PatientInfoDto
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string IdentificationNumber { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public record VaccinationRecordDto
{
    public Guid Id { get; init; }
    public Guid VaccineId { get; init; }
    public string VaccineName { get; init; } = string.Empty;
    public string VaccineCode { get; init; } = string.Empty;
    public DateTime ApplicationDate { get; init; }
    public int Dose { get; init; }
    public string? Lot { get; init; }
    public string? Observations { get; init; }
    public DateTime RegisteredAt { get; init; }
    public DateTime CreatedAt { get; init; }
}

public record VaccinationSummaryDto
{
    public int TotalVaccines { get; init; }
    public int TotalDoses { get; init; }
    public DateTime? LastVaccinationDate { get; init; }
    public DateTime? FirstVaccinationDate { get; init; }
    public Dictionary<string, int> VaccinesByType { get; init; } = new();
}