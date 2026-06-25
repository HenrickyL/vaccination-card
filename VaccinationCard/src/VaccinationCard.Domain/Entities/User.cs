using VaccinationCard.Domain.Common;
using VaccinationCard.Domain.Enums;

namespace VaccinationCard.Domain.Entities;

public class User: BaseEntity
{
    public string Email { get;  set; }
    public string PasswordHash { get;  set; }
    public AccountStatus Status { get;  set; } = AccountStatus.Active;
    public Guid? PatientId { get;  set; } // Ref Patient FK
    public UserRole Role { get;  set; }
    public bool IsActive { get;  set; } = true;

    public User() {} // EF
    public User(string email, string passwordHash, UserRole role = UserRole.Patient) : base()
    {
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        IsActive = true;
    }
    public void LinkToPatient(Guid patientId)
    {
        PatientId = patientId;
        UpdateTimestamp();
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
