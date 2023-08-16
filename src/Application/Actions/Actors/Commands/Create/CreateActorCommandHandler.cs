using MediatR;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Actors.Commands.Create;

public class CreateActorCommandHandler : IRequestHandler<CreateActorCommand, int>
{
    private readonly IActorService _actorService;

    public CreateActorCommandHandler(IActorService actorService)
    {
        _actorService = actorService;
    }

    public async Task<int> Handle(CreateActorCommand request, CancellationToken cancellationToken)
    {
        var actor = new Actor()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Biography = request.Biography,
            PhotoLink = null
        };

        await _actorService.Create(actor, request.PhotoFile, cancellationToken);

        return actor.Id;
    }
}
