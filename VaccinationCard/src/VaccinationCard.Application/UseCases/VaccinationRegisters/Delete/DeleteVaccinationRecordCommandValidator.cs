using FluentValidation;

namespace VaccinationCard.Application.UseCases.VaccinationRegisters.Delete;

public class DeleteVaccinationRecordCommandValidator : AbstractValidator<DeleteVaccinationRecordCommand>
{
    public DeleteVaccinationRecordCommandValidator() {

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Email is required");
    }
}
