using MediatR;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Seasons.Commands.Update;

public class UpdateSeasonCommandHandler : IRequestHandler<UpdateSeasonCommand>
{
    private readonly ISeasonService _seasonService;

    public UpdateSeasonCommandHandler(ISeasonService seasonService)
    {
        _seasonService = seasonService;
    }

    public async Task Handle(UpdateSeasonCommand request, CancellationToken cancellationToken)
    {
        var season = new Season()
        {
            Id = request.SeasonId,
            Title = request.Title,
            Description = request.Description,
            Order = request.Order,
            TrailerLink = request.TrailerLink
        };

        await _seasonService.Update(season, request.PosterFile, cancellationToken);
    }
}
