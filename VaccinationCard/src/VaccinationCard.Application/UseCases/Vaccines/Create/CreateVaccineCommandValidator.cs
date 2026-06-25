using FluentValidation;

namespace VaccinationCard.Application.UseCases.Vaccines.Create;

public class CreateVaccineCommandValidator: AbstractValidator<CreateVaccineCommand>
{
    public CreateVaccineCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Vaccine name is required")
            .MinimumLength(3).WithMessage("\"Vaccine name must be at least 3 characters long")
            .MaximumLength(63).WithMessage("Vaccine name must not exceed 64 characters");

        RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Vaccine Description is required")
                .MaximumLength(255).WithMessage("Identification number must not exceed 255 characters");

        RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Vaccine Code is required")
                .MaximumLength(15).WithMessage("Email must not exceed 16 characters");
    }
}
