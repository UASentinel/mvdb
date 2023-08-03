using FluentValidation;

namespace MvDb.Application.Actions.Episodes.Commands.Update;

public class UpdateEpisodeCommandValidator : AbstractValidator<UpdateEpisodeCommand>
{
    public UpdateEpisodeCommandValidator()
    {
        RuleFor(e => e.Title)
            .MaximumLength(150);

        RuleFor(e => e.Description)
            .MaximumLength(300);

        RuleFor(e => e.Duration)
            .NotEmpty();

        RuleFor(e => e.ReleaseDate)
            .NotEmpty();

        RuleFor(e => e.Order)
            .NotEmpty();
    }
}
