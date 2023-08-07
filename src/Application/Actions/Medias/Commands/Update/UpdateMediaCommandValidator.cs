using FluentValidation;

namespace MvDb.Application.Actions.Medias.Commands.Update;

public class UpdateMediaCommandValidator : AbstractValidator<UpdateMediaCommand>
{
    public UpdateMediaCommandValidator()
    {
        RuleFor(s => s.Title)
            .MaximumLength(150);

        RuleFor(s => s.Description)
            .MaximumLength(500);

        RuleFor(s => s.AgeRatingId)
            .NotEmpty();
    }
}
