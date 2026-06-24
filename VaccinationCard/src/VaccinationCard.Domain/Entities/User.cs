using VaccinationCard.Domain.Common;

namespace VaccinationCard.Domain.Entities;

public class User :BaseEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; }

    private User() { } // EF

    public User(string name, string email)
    {
        Name = name;
        Email = email;
    }
}
