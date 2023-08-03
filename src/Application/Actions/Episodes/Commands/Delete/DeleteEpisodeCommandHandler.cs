using MediatR;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Episodes.Commands.Delete;

public class DeleteEpisodeCommandHandler : IRequestHandler<DeleteEpisodeCommand>
{
    private readonly IEpisodeService _episodeService;

    public DeleteEpisodeCommandHandler(IEpisodeService episodeService)
    {
        _episodeService = episodeService;
    }

    public async Task Handle(DeleteEpisodeCommand request, CancellationToken cancellationToken)
    {
        await _episodeService.Delete(request.Id, cancellationToken);
    }
}
