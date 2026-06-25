using FluentValidation;

namespace VaccinationCard.Application.UseCases.VaccinationRegisters.Create;

public class CreateVaccinationRegisterCommandValidator : AbstractValidator<CreateVaccinationRegisterCommand>
{
    public CreateVaccinationRegisterCommandValidator() 
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("Patient ID is required");

        RuleFor(x => x.VaccineId)
            .NotEmpty().WithMessage("Vaccine ID is required");

        RuleFor(x => x.ApplicationDate)
            .NotEmpty().WithMessage("Application date is required")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Application date cannot be in the future");

        RuleFor(x => x.Dose)
            .GreaterThan(0).WithMessage("Dose must be greater than 0");

        RuleFor(x => x.Lot)
            .MaximumLength(50).WithMessage("Lot must not exceed 50 characters");

        RuleFor(x => x.Observations)
            .MaximumLength(500).WithMessage("Observations must not exceed 500 characters");
    }
}
