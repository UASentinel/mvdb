using FluentValidation;

namespace MvDb.Application.Actions.AgeRatings.Commands.Update;

public class UpdateAgeRatingCommandValidator : AbstractValidator<UpdateAgeRatingCommand>
{
    public UpdateAgeRatingCommandValidator()
    {
        RuleFor(a => a.Name)
            .MaximumLength(20)
            .NotEmpty();

        RuleFor(a => a.MinAge)
            .NotEmpty();
    }
}
