using Moq;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Repositories;

namespace VaccinationCard.Tests.Common.Mocks;

public static class MockPatientRepository
{
    public static Mock<IPatientRepository> GetMock()
    {
        var list = new List<Patient>();
        var mock = new Mock<IPatientRepository>();

        mock.Setup(r => r.Add(It.IsAny<Patient>()))
            .Callback<Patient>(patient => {
                list.Add(patient);
            });

        mock.Setup(r => r.Update(It.IsAny<Patient>()))
            .Callback<Patient>(patient => {
                var u = list.FirstOrDefault(x => x.Id == patient.Id);
                u.FullName = patient.FullName;
                u.IdentificationNumber = patient.IdentificationNumber;
            });

        mock.Setup(r => r.ExistsByIdentificationNumberAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string number, CancellationToken ct) => false);

        return mock;
    }
}
