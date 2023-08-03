using MediatR;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Actors.Commands.Update;

public class UpdateActorCommandHandler : IRequestHandler<UpdateActorCommand>
{
    private readonly IActorService _actorService;

    public UpdateActorCommandHandler(IActorService actorService)
    {
        _actorService = actorService;
    }

    public async Task Handle(UpdateActorCommand request, CancellationToken cancellationToken)
    {
        var actor = new Actor()
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Biography = request.Biography,
            PhotoLink = null
        };

        await _actorService.Update(actor, request.PhotoFile, cancellationToken);
    }
}
