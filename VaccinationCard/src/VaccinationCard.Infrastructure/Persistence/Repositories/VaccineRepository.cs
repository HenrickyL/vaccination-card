using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Repositories;

namespace VaccinationCard.Infrastructure.Persistence.Repositories;

public class VaccineRepository : IVaccineRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Vaccine> _dbSet;

    public VaccineRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<Vaccine>();
    }
    public void Add(Vaccine vaccine)
    {
        _dbSet.Add(vaccine);
    }

    public void Delete(Vaccine vaccine)
    {
        vaccine.SoftDelete();
        _dbSet.Update(vaccine);
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        bool exists = await _dbSet.AsNoTracking().AnyAsync(x=>x.Id == id, cancellationToken);
        return exists;
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        bool exists = await _dbSet.AsNoTracking().AnyAsync(x=>x.Name.ToLower() == name.ToLower(), cancellationToken);
        return exists;
    }

    public async Task<IEnumerable<Vaccine>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Vaccine?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Vaccine? vaccine = await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return vaccine;
    }

    public void Update(Vaccine vaccine)
    {
        _dbSet.Update(vaccine);
    }
}
