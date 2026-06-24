using VaccinationCard.Domain.Common;

namespace VaccinationCard.Domain.Entities;

public class Patient :BaseEntity
{
    public string FullName { get; private set; }
    public string IdentificationNumber { get; private set; }


    private Patient() { } // EF

    public Patient(string name, string identificationNumber)
    {
        this.FullName = name;
        this.IdentificationNumber = identificationNumber;
    }
}
