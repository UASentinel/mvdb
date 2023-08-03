using MediatR;
using MvDb.Application.Actions.Episodes.DataTransferObjects;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Actions.Seasons.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Seasons.Queries.Get;

public class GetSeasonsCommandHandler : IRequestHandler<GetSeasonsQuery, ICollection<SeasonDto>>
{
    private readonly ISeasonService _seasonService;

    public GetSeasonsCommandHandler(ISeasonService seasonService)
    {
        _seasonService = seasonService;
    }

    public async Task<ICollection<SeasonDto>> Handle(GetSeasonsQuery request, CancellationToken cancellationToken)
    {
        var seasons = _seasonService.Get();

        if (seasons == null)
            return new List<SeasonDto>();

        var seasonDtos = new List<SeasonDto>();
        foreach (var season in seasons)
        {
            var seasonDto = new SeasonDto()
            {
                Id = season.Id,
                Title = season.Title,
                Description = season.Description,
                Order = season.Order,
                PosterLink = season.PosterLink,
                TrailerLink = season.TrailerLink
            };
            seasonDtos.Add(seasonDto);
        }

        return seasonDtos;
    }
}
