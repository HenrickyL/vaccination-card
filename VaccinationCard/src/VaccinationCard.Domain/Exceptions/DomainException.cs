using VaccinationCard.Domain.Enums;

namespace VaccinationCard.Domain.Exceptions;

public abstract class DomainException : Exception
{
    public string Details { get; protected set; }
    public ErrorCode ErrorCode { get; protected set; }


    public DomainException(string details, ErrorCode errorCode) : base(details) {
        this.Details = details;
        this.ErrorCode = errorCode;
    }
}

