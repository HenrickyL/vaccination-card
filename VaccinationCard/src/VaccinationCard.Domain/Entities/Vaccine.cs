using VaccinationCard.Domain.Common;

namespace VaccinationCard.Domain.Entities;

public class Vaccine : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Code { get; set; }
    //Navigation
    public virtual ICollection<VaccineRegister> Registrations { get; private set; }

    private Vaccine() { } // EF Constructor

    public Vaccine(string name, string code, string description) 
    {
        this.Name = name;
        this.Description = description;
        this.Code = code;
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
