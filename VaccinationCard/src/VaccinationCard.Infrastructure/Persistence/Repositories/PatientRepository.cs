using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Repositories;

namespace VaccinationCard.Infrastructure.Persistence.Repositories;

public class PatientRepository : IPatientRepository
{
    public void Add(Patient patient)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByIdentificationNumberAsync(string number, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Patient?> GetByIdentificationNumberAsync(string number, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Update(Patient patient)
    {
        throw new NotImplementedException();
    }
}
