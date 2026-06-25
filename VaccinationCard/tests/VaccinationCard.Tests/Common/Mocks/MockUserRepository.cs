using Moq;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Repositories;

namespace VaccinationCard.Tests.Common.Mocks;

public static class MockUserRepository
{
    public static Mock<IUserRepository> GetMock()
    {
        var users = new List<User>();
        var mock = new Mock<IUserRepository>();

        mock.Setup(r => r.Add(It.IsAny<User>()))
            .Callback<User>(user => {
                users.Add(user);
            });

        mock.Setup(r => r.Update(It.IsAny<User>()))
            .Callback<User>(user => {
                var u = users.FirstOrDefault(x => x.Id == user.Id);
                u.Email = user.Email;
                u.Status = user.Status;
                u.Role = user.Role;
            });

        mock.Setup(r => r.ExistsByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string email, CancellationToken ct) => false);

        mock.Setup(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        return mock;
    }
}
