using FluentValidation;

namespace MvDb.Application.Actions.Directors.Commands.Update;

public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
{
    public UpdateDirectorCommandValidator()
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
