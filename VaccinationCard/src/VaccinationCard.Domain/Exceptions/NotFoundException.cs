
using VaccinationCard.Domain.Enums;

namespace VaccinationCard.Domain.Exceptions;
public class NotFoundException : DomainException
{
    public NotFoundException(string details = "NotFoundException", ErrorCode errorCode = ErrorCode.NotFoundError) : base(details, errorCode)
    {}
}
