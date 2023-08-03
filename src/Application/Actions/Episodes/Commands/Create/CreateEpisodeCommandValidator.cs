﻿using FluentValidation;

namespace MvDb.Application.Actions.Episodes.Commands.Create;

public class CreateEpisodeCommandValidator : AbstractValidator<CreateEpisodeCommand>
{
    public CreateEpisodeCommandValidator()
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

        RuleFor(e => e.SeasonId)
            .NotEmpty();
    }
}
