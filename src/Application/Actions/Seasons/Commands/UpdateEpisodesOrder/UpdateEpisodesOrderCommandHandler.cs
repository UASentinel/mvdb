using MediatR;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Seasons.Commands.UpdateEpisodesOrder;

public class UpdateEpisodesOrderCommandHandler : IRequestHandler<UpdateEpisodesOrderCommand>
{
    private readonly IEpisodeService _episodeService;

    public UpdateEpisodesOrderCommandHandler(IEpisodeService episodeService)
    {
        _episodeService = episodeService;
    }

    public async Task Handle(UpdateEpisodesOrderCommand request, CancellationToken cancellationToken)
    {
        var seasonEpisodes = new List<Episode>();
        foreach (var seasonEpisodeDto in request.SeasonEpisodeDtos)
        {
            seasonEpisodes.Add(new Episode()
            {
                Id = seasonEpisodeDto.EpisodeId,
                Order = seasonEpisodeDto.Order
            });
        }

        await _episodeService.UpdateEpisodesOrder(seasonEpisodes, cancellationToken);
    }
}
