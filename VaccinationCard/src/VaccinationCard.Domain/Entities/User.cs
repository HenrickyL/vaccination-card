using VaccinationCard.Domain.Common;
using VaccinationCard.Domain.Enums;
using VaccinationCard.Domain.Exceptions;

namespace VaccinationCard.Domain.Entities;

public class User: BaseEntity
{
    public string Email { get;  set; }
    public string PasswordHash { get;  set; }
    public AccountStatus Status { get;  set; } = AccountStatus.Active;
    public Guid? PatientId { get;  set; } // Ref Patient FK
    private UserRole _role;
    public UserRole Role
    {
        get => _role;
        set
        {
            if (_role == UserRole.Admin && value != UserRole.Admin)
                throw new ForbiddenException("SuperAdmin cannot change its own role");

            if (value == UserRole.Admin)
                throw new ForbiddenException("Cannot assign Admin role");
            if (value < _role)
            {
                throw new ForbiddenException("Cannot assign a role greater than the current one");
            }
            _role = value;
            UpdateTimestamp();
        }
    }
    public bool IsActive { get;  set; } = true;

    public User() {} // EF
    public User(string email, string passwordHash, UserRole? role = UserRole.Patient) : base()
    {
        Email = email;
        PasswordHash = passwordHash;
        Role = role ?? UserRole.Patient;
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

    public bool CanCreateEmployee()
    {
        return Role == UserRole.Admin;
    }

    public void Activate()
    {
        IsActive = true;
        UpdateTimestamp();
    }

    // Navigation property
    public Patient? Patient { get; set; }
}
