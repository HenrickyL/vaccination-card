using FluentValidation;
using VaccinationCard.Application.UseCases.Vaccines.Lis;

namespace VaccinationCard.Application.UseCases.Vaccines.List;

public class ListVaccinesCommandValidator : AbstractValidator<ListVaccinesCommand>
{
    public ListVaccinesCommandValidator() {
    }
}
