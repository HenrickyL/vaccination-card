
using VaccinationCard.Domain.Common;

namespace VaccinationCard.Domain.Entities;

/// <summary>
/// Relation N:N between Patient and Vaccine
/// </summary>
public class VaccineRegister : BaseEntity
{
    // Foreign Keys (N:N relationship)
    public Guid PatientId { get; set; }
    public Guid VaccineId { get; set; }

    // Additional attributes
    public DateTime ApplicationDate { get; set; }
    public int Dose { get; set; }
    public string Lot { get; set; }
    public string Observations { get; set; }
    public DateTime RegisteredAt { get; private set; }

    // Navigation properties (N:1)
    public virtual Patient Patient { get; private set; }
    public virtual Vaccine Vaccine { get; private set; }

    private VaccineRegister() { } // EF

    public VaccineRegister(
        Guid personId,
        Guid vaccineId,
        DateTime applicationDate,
        int dose,
        string lot = null,
        string observations = null)
    {
        PatientId = personId;
        VaccineId = vaccineId;
        ApplicationDate = applicationDate;
        Dose = dose;
        Lot = lot;
        Observations = observations;
        RegisteredAt = DateTime.UtcNow;
    }

    public void Update(DateTime applicationDate, int dose, string lot, string observations)
    {
        ApplicationDate = applicationDate;
        Dose = dose;
        Lot = lot;
        Observations = observations;
    }
}
