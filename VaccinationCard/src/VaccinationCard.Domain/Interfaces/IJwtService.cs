using VaccinationCard.Domain.Enums;

namespace VaccinationCard.Domain.Interfaces;

public interface IJwtService
{
    string GenerateToken(Guid userId, string email, UserRole role);
}
