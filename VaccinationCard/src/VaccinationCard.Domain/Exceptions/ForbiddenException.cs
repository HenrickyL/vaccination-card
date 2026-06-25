using VaccinationCard.Domain.Enums;

namespace VaccinationCard.Domain.Exceptions;

public class ForbiddenException : DomainException
{
    public ForbiddenException(string details = "Forbidden", ErrorCode errorCode = ErrorCode.ForbiddenError)
        : base(details, errorCode)
    { }
}
