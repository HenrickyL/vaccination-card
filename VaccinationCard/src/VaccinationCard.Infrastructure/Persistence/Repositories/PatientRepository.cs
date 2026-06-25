using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Repositories;

namespace VaccinationCard.Infrastructure.Persistence.Repositories;

public class PatientRepository : IPatientRepository
{

    private readonly AppDbContext _context;
    private readonly DbSet<Patient> _dbSet;

    public PatientRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<Patient>();
    }

    public void Add(Patient patient)
    {
        _dbSet.Add(patient);
    }

    public async Task<bool> ExistsByIdentificationNumberAsync(string number, CancellationToken cancellationToken = default)
    {
        bool exists = await _dbSet.AsNoTracking().AnyAsync(x => x.IdentificationNumber.ToLower() == number.ToLower(), cancellationToken);
        return exists;
    }

    public async Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Patient? patient = await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return patient;
    }

    public async Task<Patient?> GetByIdentificationNumberAsync(string number, CancellationToken cancellationToken = default)
    {
        Patient? patient = await _dbSet.FirstOrDefaultAsync(x => x.IdentificationNumber.ToLower() == number.ToLower(), cancellationToken);
        return patient;

    }

    public void Update(Patient patient)
    {
        _dbSet.Update(patient);
    }
}
