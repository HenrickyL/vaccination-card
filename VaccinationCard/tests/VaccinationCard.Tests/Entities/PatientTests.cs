using FluentAssertions;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Tests.Entities;

public class PatientTests
{
    [Fact]
    public void Constructor_ShouldCreatePatientWithValidData()
    {
        // Arrange
        var fullName = "John Doe";
        var identificationNumber = "12345";
        // Act
        var patient = new Patient(fullName, identificationNumber);
        // Assert
        patient.FullName.Should().Be(fullName);
        patient.IdentificationNumber.Should().Be(identificationNumber);
        patient.Id.Should().NotBeEmpty();
        patient.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}
