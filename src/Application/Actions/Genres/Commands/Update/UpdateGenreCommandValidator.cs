using FluentValidation;

namespace MvDb.Application.Actions.Genres.Commands.Update;

public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(g => g.Name)
            .MaximumLength(20)
            .NotEmpty();
    }
}
