using FluentValidation;

namespace MvDb.Application.Actions.Genres.Commands.Update;

public class CreateUpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public CreateUpdateGenreCommandValidator()
    {
        RuleFor(g => g.Name)
            .MaximumLength(20)
            .NotEmpty();
    }
}
