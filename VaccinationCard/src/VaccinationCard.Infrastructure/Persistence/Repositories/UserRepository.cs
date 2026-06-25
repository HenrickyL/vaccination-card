using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Repositories;

namespace VaccinationCard.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    public void Add(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Update(User user)
    {
        throw new NotImplementedException();
    }
}
