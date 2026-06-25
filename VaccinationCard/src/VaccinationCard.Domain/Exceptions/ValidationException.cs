using VaccinationCard.Domain.Enums;

namespace VaccinationCard.Domain.Exceptions;

public class ValidationException : DomainException
{
    public string Field { get; protected set; }
    public ValidationException(string field,string details = "ValidationException", ErrorCode errorCode = ErrorCode.ValidationError) : base(details, errorCode)
    {
        this.Field = field;
    }
}
