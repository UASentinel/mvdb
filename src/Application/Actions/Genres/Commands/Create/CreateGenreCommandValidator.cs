using FluentValidation;

namespace MvDb.Application.Genres.Commands.Create;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(g => g.Name)
            .MaximumLength(20)
            .NotEmpty();
    }
}
