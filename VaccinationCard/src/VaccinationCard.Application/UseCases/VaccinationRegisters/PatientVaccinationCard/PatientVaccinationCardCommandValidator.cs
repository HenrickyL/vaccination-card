using FluentValidation;

namespace VaccinationCard.Application.UseCases.VaccinationRegisters.List;

public class PatientVaccinationCardCommandValidator : AbstractValidator<PatientVaccinationCardCommand>
{
    public PatientVaccinationCardCommandValidator() {
        RuleFor(x => x.PatientId)
           .NotEmpty().WithMessage("Patient ID is required");
    }
}
