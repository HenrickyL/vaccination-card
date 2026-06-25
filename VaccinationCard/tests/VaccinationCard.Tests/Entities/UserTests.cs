using FluentAssertions;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Enums;

namespace VaccinationCard.Tests.Entities;

public  class UserTests
{
    [Fact]
    public void Constructor_ShouldCreateUserWithValidData()
    {
        // Arrange
        var email = "john@example.com";
        var passwordHash = "hashed_password";
        var role = UserRole.Patient;

        // Act
        var user = new User(email, passwordHash, role);

        // Assert
        user.Email.Should().Be(email);
        user.PasswordHash.Should().Be(passwordHash);
        user.Role.Should().Be(role);
        user.IsActive.Should().BeTrue();
        user.Id.Should().NotBeEmpty();
        user.PatientId.Should().BeNull();
    }

    [Fact]
    public void Constructor_ShouldSetDefaultRoleToPatient_WhenRoleNotProvided()
    {
        // Act
        var user = new User("john@example.com", "hashed_password");

        // Assert
        user.Role.Should().Be(UserRole.Patient);
    }

    [Fact]
    public void LinkToPatient_ShouldSetPatientId()
    {
        // Arrange
        var user = new User("john@example.com", "hashed_password");
        var patientId = Guid.NewGuid();

        // Act
        user.LinkToPatient(patientId);

        // Assert
        user.PatientId.Should().Be(patientId);
        user.UpdatedAt.Should().NotBeNull();
    }


    [Fact]
    public void Deactivate_ShouldSetIsActiveToFalse()
    {
        // Arrange
        var user = new User("john@example.com", "hashed_password");

        // Act
        user.Deactivate();

        // Assert
        user.IsActive.Should().BeFalse();
        user.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public void Activate_ShouldSetIsActiveToTrue()
    {
        // Arrange
        var user = new User("john@example.com", "hashed_password");
        user.Deactivate();

        // Act
        user.Activate();

        // Assert
        user.IsActive.Should().BeTrue();
        user.UpdatedAt.Should().NotBeNull();
    }
}
