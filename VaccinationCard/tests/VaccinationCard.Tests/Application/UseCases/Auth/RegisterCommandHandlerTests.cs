
using AutoFixture;
using FluentAssertions;
using Moq;
using VaccinationCard.Application.UseCases.Auth;
using VaccinationCard.Domain.Common;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Enums;
using VaccinationCard.Domain.Interfaces;
using VaccinationCard.Domain.Repositories;
using VaccinationCard.Tests.Common.Mocks;

namespace VaccinationCard.Tests.Application.UseCases.Auth;

public class RegisterCommandHandlerTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IPatientRepository> _patientRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly RegisterCommandHandler _handler;

    public RegisterCommandHandlerTests()
    {
        _fixture = new Fixture();
        _patientRepositoryMock = MockPatientRepository.GetMock();
        _userRepositoryMock = MockUserRepository.GetMock();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _jwtServiceMock = new Mock<IJwtService>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new RegisterCommandHandler(
            _patientRepositoryMock.Object,
            _userRepositoryMock.Object,
            _passwordHasherMock.Object,
            _jwtServiceMock.Object,
            _unitOfWorkMock.Object
        );
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldCreateUserAndReturnToken()
    {
        // Arrange
        var command = _fixture.Create<RegisterCommand>();
        var patientId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var token = _fixture.Create<string>();

        _passwordHasherMock
            .Setup(x => x.Hash(command.Password))
            .Returns("hashed_password");

        _jwtServiceMock
            .Setup(x => x.GenerateToken(It.IsAny<Guid>(), command.Email, UserRole.Patient))
            .Returns(token);

        _patientRepositoryMock
            .Setup(x => x.Add(It.IsAny<Patient>()))
            .Callback<Patient>(p =>
            {
                var field = typeof(BaseEntity).GetField("_id", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                field?.SetValue(p, patientId);
            });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(command.Email);
        result.FullName.Should().Be(command.FullName);
        result.AccessToken.Should().Be(token);
        result.Role.Should().Be(UserRole.Patient.ToString());

        _userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
        _patientRepositoryMock.Verify(x => x.Add(It.IsAny<Patient>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _jwtServiceMock.Verify(x => x.GenerateToken(It.IsAny<Guid>(), command.Email, UserRole.Patient), Times.Once);
    }

   
}
