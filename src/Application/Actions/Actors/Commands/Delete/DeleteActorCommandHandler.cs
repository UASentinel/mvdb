using MediatR;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Actors.Commands.Delete;

public class DeleteActorCommandHandler : IRequestHandler<DeleteActorCommand>
{
    private readonly IActorService _actorService;

    public DeleteActorCommandHandler(IActorService actorService)
    {
        _actorService = actorService;
    }

    public async Task Handle(DeleteActorCommand request, CancellationToken cancellationToken)
    {
        await _actorService.Delete(request.Id, cancellationToken);
    }
}
