using MediatR;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Medias.Commands.UpdateDirectors;

public class UpdateDirectorsCommandHandler : IRequestHandler<UpdateDirectorsCommand>
{
    private readonly IMediaService _mediaService;

    public UpdateDirectorsCommandHandler(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    public async Task Handle(UpdateDirectorsCommand request, CancellationToken cancellationToken)
    {
        var mediaDirectors = new List<MediaDirector>();
        foreach (var mediaDirectorDto in request.MediaDirectorDtos)
        {
            mediaDirectors.Add(new MediaDirector()
            {
                MediaId = request.MediaId,
                DirectorId = mediaDirectorDto.DirectorId,
                Order = mediaDirectorDto.Order
            });
        }

        await _mediaService.UpdateDirectors(request.MediaId, mediaDirectors, cancellationToken);
    }
}
