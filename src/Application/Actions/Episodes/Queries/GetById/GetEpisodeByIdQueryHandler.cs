using MediatR;
using MvDb.Application.Actions.Episodes.DataTransferObjects;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Episodes.Queries.GetById;

public class GetEpisodeByIdQueryHandler : IRequestHandler<GetEpisodeByIdQuery, EpisodeDto>
{
    private readonly IEpisodeService _episodeService;

    public GetEpisodeByIdQueryHandler(IEpisodeService episodeService)
    {
        _episodeService = episodeService;
    }

    public async Task<EpisodeDto> Handle(GetEpisodeByIdQuery request, CancellationToken cancellationToken)
    {
        var episode = await _episodeService.GetById(request.Id);

        if (episode == null)
            return null;

        var episodeDto = new EpisodeDto()
        {
            Id = episode.Id,
            Title = episode.Title,
            Description = episode.Description,
            Duration = episode.Duration,
            Order = episode.Order,
            ReleaseDate = episode.ReleaseDate
        };
        return episodeDto;
    }
}
