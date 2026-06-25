namespace VaccinationCard.Application.UseCases.VaccinationRegisters.Update;

public record UpdateVaccinationRecordResponse
{
    public Guid Id { get; init; }
    public Guid PatientId { get; init; }
    public string PatientName { get; init; } = string.Empty;
    public Guid VaccineId { get; init; }
    public string VaccineName { get; init; } = string.Empty;
    public DateTime ApplicationDate { get; init; }
    public int Dose { get; init; }
    public string? Lot { get; init; }
    public string? Observations { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
