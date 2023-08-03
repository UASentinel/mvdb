using FluentValidation;

namespace MvDb.Application.Actions.Directors.Commands.Create;

public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
{
    public CreateDirectorCommandValidator()
    {
        RuleFor(d => d.FirstName)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(d => d.LastName)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(d => d.Biography)
            .MaximumLength(1000);
    }
}
