using MediatR;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Seasons.Commands.Create;

public class CreateSeasonCommandHandler : IRequestHandler<CreateSeasonCommand, int>
{
    private readonly ISeasonService _seasonService;

    public CreateSeasonCommandHandler(ISeasonService seasonService)
    {
        _seasonService = seasonService;
    }

    public async Task<int> Handle(CreateSeasonCommand request, CancellationToken cancellationToken)
    {
        var season = new Season()
        {
            Title = request.Title,
            Description = request.Description,
            Order = request.Order,
            TrailerLink = request.TrailerLink,
            MediaId = request.MediaId
        };

        await _seasonService.Create(season, request.PosterFile, cancellationToken);

        return season.Id;
    }
}
