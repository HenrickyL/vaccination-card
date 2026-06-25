using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Repositories;

namespace VaccinationCard.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{

    private readonly AppDbContext _context;
    private readonly DbSet<User> _dbSet;

    public UserRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<User>();
    }
    public void Add(User user)
    {
        _dbSet.Add(user);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        bool exists = await _dbSet.AsNoTracking().AnyAsync(x =>x.Email.ToLower()==email.ToLower(), cancellationToken);
        return exists;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        User? user = await _dbSet.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower(), cancellationToken);
        return user;
    }

    public void Update(User user)
    {
        _dbSet.Update(user);
    }
}
