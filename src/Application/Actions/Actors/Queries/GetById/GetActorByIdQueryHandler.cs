using MediatR;
using MvDb.Application.Actions.Actors.DataTransferObjects;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Actors.Queries.GetById;

public class GetActorByIdQueryHandler : IRequestHandler<GetActorByIdQuery, ActorDto>
{
    private readonly IActorService _actorService;

    public GetActorByIdQueryHandler(IActorService actorService)
    {
        _actorService = actorService;
    }

    public async Task<ActorDto> Handle(GetActorByIdQuery request, CancellationToken cancellationToken)
    {
        var actor = await _actorService.GetById(request.Id);

        if (actor == null)
            return null;

        var actorDto = new ActorDto()
        {
            Id = actor.Id,
            FirstName = actor.FirstName,
            LastName = actor.LastName,
            DateOfBirth = actor.DateOfBirth,
            Biography = actor.Biography,
            PhotoLink = actor.PhotoLink
        };
        return actorDto;
    }
}
