using MediatR;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Medias.Commands.Delete;

public class DeleteMediaCommandHandler : IRequestHandler<DeleteMediaCommand>
{
    private readonly IMediaService _mediaService;

    public DeleteMediaCommandHandler(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    public async Task Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
    {
        await _mediaService.Delete(request.Id, cancellationToken);
    }
}
