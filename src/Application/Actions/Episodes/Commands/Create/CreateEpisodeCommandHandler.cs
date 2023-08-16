using MediatR;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Episodes.Commands.Create;

public class CreateEpisodeCommandHandler : IRequestHandler<CreateEpisodeCommand, int>
{
    private readonly IEpisodeService _episodeService;

    public CreateEpisodeCommandHandler(IEpisodeService episodeService)
    {
        _episodeService = episodeService;
    }

    public async Task<int> Handle(CreateEpisodeCommand request, CancellationToken cancellationToken)
    {
        var episode = new Episode()
        {
            Title = request.Title,
            Description = request.Description,
            Duration = request.Duration,
            Order = request.Order,
            ReleaseDate = request.ReleaseDate,
            SeasonId = request.SeasonId
        };

        await _episodeService.Create(episode, cancellationToken);

        return episode.Id;
    }
}
