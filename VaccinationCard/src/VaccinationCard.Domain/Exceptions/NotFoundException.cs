
namespace VaccinationCard.Domain.Exceptions;
public class NotFoundException : DomainException
{
    public NotFoundException(string message = "NotFoundException") : base(message)
    {}
}
