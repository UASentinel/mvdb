using FluentValidation;

namespace MvDb.Application.Actions.Actors.Commands.Update;

public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
{
    public UpdateActorCommandValidator()
    {
        RuleFor(a => a.FirstName)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(a => a.LastName)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(a => a.Biography)
            .MaximumLength(1000);
    }
}
