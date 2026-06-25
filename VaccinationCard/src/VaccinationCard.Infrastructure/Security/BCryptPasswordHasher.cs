using VaccinationCard.Domain.Interfaces;

namespace VaccinationCard.Infrastructure.Security;

public class BCryptPasswordHasher: IPasswordHasher
{
    public string Hash(string password)
           => BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);

    public bool Verify(string hash, string password)
        => BCrypt.Net.BCrypt.Verify(password, hash);
}
