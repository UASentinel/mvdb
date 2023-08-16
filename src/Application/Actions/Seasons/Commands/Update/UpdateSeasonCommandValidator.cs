using FluentValidation;

namespace MvDb.Application.Actions.Seasons.Commands.Update;

public class UpdateSeasonCommandValidator : AbstractValidator<UpdateSeasonCommand>
{
    public UpdateSeasonCommandValidator()
    {
        RuleFor(s => s.Title)
            .MaximumLength(150);

        RuleFor(s => s.Description)
            .MaximumLength(500);

        RuleFor(s => s.Order)
            .NotEmpty();
    }
}
