using FluentValidation;

namespace MvDb.Application.Actions.AgeRatings.Commands.Create;

public class CreateAgeRatingCommandValidator : AbstractValidator<CreateAgeRatingCommand>
{
    public CreateAgeRatingCommandValidator()
    {
        RuleFor(a => a.Name)
            .MaximumLength(20)
            .NotEmpty();

        RuleFor(a => a.MinAge)
            .NotEmpty();
    }
}
