using MediatR;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Medias.Commands.UpdateActors;

public class UpdateActorsCommandHandler : IRequestHandler<UpdateActorsCommand>
{
    private readonly IMediaService _mediaService;

    public UpdateActorsCommandHandler(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    public async Task Handle(UpdateActorsCommand request, CancellationToken cancellationToken)
    {
        var mediaActors = new List<MediaActor>();
        foreach (var mediaDirectorDto in request.MediaActorDtos)
        {
            mediaActors.Add(new MediaActor()
            {
                MediaId = request.MediaId,
                ActorId = mediaDirectorDto.ActorId,
                Order = mediaDirectorDto.Order
            });
        }

        await _mediaService.UpdateActors(request.MediaId, mediaActors, cancellationToken);
    }
}
