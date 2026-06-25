
namespace VaccinationCard.Domain.Enums;

public  enum ErrorCode{
    NotFoundError,
    BadRequestError,
    ValidationError,
    ForbiddenError,
    InternalError,
    // --------
    EmailAlreadyExist,
    IdentificationNumberAlreadyExist,
    InvalidCredentials,
}
