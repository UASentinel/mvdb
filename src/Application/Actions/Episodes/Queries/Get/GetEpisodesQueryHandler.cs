using MediatR;
using MvDb.Application.Actions.Episodes.DataTransferObjects;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Episodes.Queries.Get;

public class GetEpisodesCommandHandler : IRequestHandler<GetEpisodesQuery, ICollection<EpisodeDto>>
{
    private readonly IEpisodeService _episodeService;

    public GetEpisodesCommandHandler(IEpisodeService episodeService)
    {
        _episodeService = episodeService;
    }

    public async Task<ICollection<EpisodeDto>> Handle(GetEpisodesQuery request, CancellationToken cancellationToken)
    {
        var episodes = _episodeService.Get();

        if (episodes == null)
            return new List<EpisodeDto>();

        var episodeDtos = new List<EpisodeDto>();
        foreach (var episode in episodes)
        {
            var episodeDto = new EpisodeDto()
            {
                Id = episode.Id,
                Title = episode.Title,
                Description = episode.Description,
                Duration = episode.Duration,
                Order = episode.Order,
                ReleaseDate = episode.ReleaseDate
            };
            episodeDtos.Add(episodeDto);
        }

        return episodeDtos;
    }
}
