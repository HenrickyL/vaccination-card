using VaccinationCard.Domain.Common;

namespace VaccinationCard.Domain.Entities;

public class Patient :BaseEntity
{
    public string FullName { get; set; }
    public string IdentificationNumber { get; set; }


    private Patient() { } // EF

    public Patient(string name, string identificationNumber)
    {
        this.FullName = name;
        this.IdentificationNumber = identificationNumber;
    }
}
