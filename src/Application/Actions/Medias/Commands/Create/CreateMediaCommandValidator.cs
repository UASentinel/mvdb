using FluentValidation;

namespace MvDb.Application.Actions.Medias.Commands.Create;

public class CreateMediaCommandValidator : AbstractValidator<CreateMediaCommand>
{
    public CreateMediaCommandValidator()
    {
        RuleFor(s => s.Title)
            .MaximumLength(150);

        RuleFor(s => s.Description)
            .MaximumLength(500);

        RuleFor(s => s.AgeRatingId)
            .NotEmpty();
    }
}
