namespace VaccinationCard.Domain.Exceptions;
public class BadRequestException : DomainException
{
    public BadRequestException(string message = "BadRequestException") : base(message)
    {}
}
