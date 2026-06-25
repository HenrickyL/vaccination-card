using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Domain.Repositories;

public interface IVaccineRepository
{
    Task<Vaccine?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Vaccine>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Vaccine vaccine);
    void Update(Vaccine vaccine);
    void Delete(Vaccine vaccine);
}
