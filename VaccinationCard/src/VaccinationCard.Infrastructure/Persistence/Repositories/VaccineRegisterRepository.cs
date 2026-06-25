using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Repositories;

namespace VaccinationCard.Infrastructure.Persistence.Repositories;

public class VaccineRegisterRepository : IVaccineRegisterRepository
{

    private readonly AppDbContext _context;
    private readonly DbSet<VaccineRegister> _dbSet;

    public VaccineRegisterRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<VaccineRegister>();
    }
    // ---------------
    public void Add(VaccineRegister record)
    {
        _dbSet.Add(record);
    }

    public void Delete(VaccineRegister record)
    {
        record.SoftDelete();
        _dbSet.Update(record);
    }

    public async Task<bool> ExistsByPatientAndVaccineAsync(Guid patientId, Guid vaccineId, CancellationToken cancellationToken = default)
    {
        bool exists = await _dbSet.AsNoTracking().AnyAsync(x=> x.PatientId == patientId && x.VaccineId == vaccineId, cancellationToken);
        return exists;
    }

    public async Task<VaccineRegister?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        VaccineRegister? entity = await _dbSet.FirstOrDefaultAsync(x=>id == id, cancellationToken);
        return entity;
    }

    public async Task<IEnumerable<VaccineRegister>> GetByPatientAndVaccineAsync(Guid patientId, Guid vaccineId, CancellationToken cancellationToken = default)
    {
        var response = await _dbSet
            .Include(r => r.Vaccine)
            .Where(r => r.PatientId == patientId && !r.IsDeleted)
            .OrderByDescending(r => r.ApplicationDate)
            .ToListAsync(cancellationToken);
        return response;
    }

    public async Task<IEnumerable<VaccineRegister>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        var query =  _dbSet
            .Include(x=>x.Vaccine)
            .Where(r => r.PatientId == patientId);

        return await query.ToListAsync(cancellationToken);
    }

    public void Update(VaccineRegister record)
    {
        _dbSet.Update(record);
    }
}
