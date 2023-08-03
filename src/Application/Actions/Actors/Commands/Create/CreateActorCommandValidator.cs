using FluentValidation;

namespace MvDb.Application.Actions.Actors.Commands.Create;

public class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
{
    public CreateActorCommandValidator()
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
