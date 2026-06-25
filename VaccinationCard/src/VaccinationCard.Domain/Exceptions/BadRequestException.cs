using VaccinationCard.Domain.Enums;

namespace VaccinationCard.Domain.Exceptions;
public class BadRequestException : DomainException
{
    public BadRequestException(string details = "BadRequestException", ErrorCode errorCode = ErrorCode.BadRequestError) : base(details, errorCode)
    {}
}
