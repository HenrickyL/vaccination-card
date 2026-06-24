using VaccinationCard.Domain.Common;
using VaccinationCard.Domain.Enums;

namespace VaccinationCard.Domain.Entities;

public class User: BaseEntity
{
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public AccountStatus Status { get; private set; } = AccountStatus.Active;
    public Guid? PatientId { get; private set; } // Ref Patient FK
    public UserRole Role { get; private set; }
    public bool IsActive { get; private set; } = true;

    public User() {} // EF
    public User(string email, string passwordHash, UserRole role = UserRole.Patient) : base()
    {
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdateTimestamp();
    }

    public void Activate()
    {
        IsActive = true;
        UpdateTimestamp();
    }

    // Navigation property
    public Patient? Patient { get; set; }
}
