using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Domain.Repositories;

public interface IPatientRepository
{
    Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Patient?> GetByIdentificationNumberAsync(string number, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdentificationNumberAsync(string number, CancellationToken cancellationToken = default);
    void Add(Patient patient);
    void Update(Patient patient);
}
