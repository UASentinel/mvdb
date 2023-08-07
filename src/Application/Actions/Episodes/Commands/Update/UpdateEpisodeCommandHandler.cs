using MediatR;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Episodes.Commands.Update;

public class UpdateEpisodeCommandHandler : IRequestHandler<UpdateEpisodeCommand>
{
    private readonly IEpisodeService _episodeService;

    public UpdateEpisodeCommandHandler(IEpisodeService episodeService)
    {
        _episodeService = episodeService;
    }

    public async Task Handle(UpdateEpisodeCommand request, CancellationToken cancellationToken)
    {
        var episode = new Episode()
        {
            Id = request.EpisodeId,
            Title = request.Title,
            Description = request.Description,
            Duration = request.Duration,
            Order = request.Order,
            ReleaseDate = request.ReleaseDate
        };

        await _episodeService.Update(episode, cancellationToken);
    }
}
