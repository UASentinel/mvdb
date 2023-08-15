using MediatR;
using MvDb.Application.Actions.Episodes.DataTransferObjects;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Actions.Seasons.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Seasons.Queries.GetById;

public class GetSeasonByIdQueryHandler : IRequestHandler<GetSeasonByIdQuery, SeasonDto>
{
    private readonly ISeasonService _seasonService;

    public GetSeasonByIdQueryHandler(ISeasonService seasonService)
    {
        _seasonService = seasonService;
    }

    public async Task<SeasonDto> Handle(GetSeasonByIdQuery request, CancellationToken cancellationToken)
    {
        var season = await _seasonService.GetById(request.Id);

        if (season == null)
            return null;

        var seasonDto = new SeasonDto()
        {
            Id = season.Id,
            Title = season.Title,
            Description = season.Description,
            Order = season.Order,
            PosterLink = season.PosterLink,
            TrailerLink = season.TrailerLink
        };

        seasonDto.SetEpisodes(season.Episodes);

        return seasonDto;
    }
}
