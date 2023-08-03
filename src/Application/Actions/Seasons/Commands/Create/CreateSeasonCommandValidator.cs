using FluentValidation;

namespace MvDb.Application.Actions.Seasons.Commands.Create;

public class CreateSeasonCommandValidator : AbstractValidator<CreateSeasonCommand>
{
    public CreateSeasonCommandValidator()
    {
        RuleFor(s => s.Title)
            .MaximumLength(150);

        RuleFor(s => s.Description)
            .MaximumLength(500);

        RuleFor(s => s.Order)
            .NotEmpty();

        RuleFor(s => s.MediaId)
            .NotEmpty();
    }
}
