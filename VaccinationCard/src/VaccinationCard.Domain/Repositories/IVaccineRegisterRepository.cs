using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Domain.Repositories;

public interface IVaccineRegisterRepository
{
    Task<VaccineRegister?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<VaccineRegister>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<VaccineRegister>> GetByPatientAndVaccineAsync(Guid patientId, Guid vaccineId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByPatientAndVaccineAsync(Guid patientId, Guid vaccineId, CancellationToken cancellationToken = default);
    void Add(VaccineRegister record);
    void Update(VaccineRegister record);
    void Delete(VaccineRegister record);
}
