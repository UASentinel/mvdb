using MediatR;
using MvDb.Application.Actions.Actors.DataTransferObjects;
using MvDb.Application.Common.Interfaces.EntityServices;

namespace MvDb.Application.Actions.Actors.Queries.Get;

public class GetActorsCommandHandler : IRequestHandler<GetActorsQuery, ICollection<ActorDto>>
{
    private readonly IActorService _actorService;

    public GetActorsCommandHandler(IActorService actorService)
    {
        _actorService = actorService;
    }

    public async Task<ICollection<ActorDto>> Handle(GetActorsQuery request, CancellationToken cancellationToken)
    {
        var actors = _actorService.Get();

        if (actors == null)
            return new List<ActorDto>();

        var actorDtos = new List<ActorDto>();
        foreach (var actor in actors)
        {
            var actorDto = new ActorDto()
            {
                Id = actor.Id,
                FirstName = actor.FirstName,
                LastName = actor.LastName,
                DateOfBirth = actor.DateOfBirth,
                Biography = actor.Biography,
                PhotoLink = actor.PhotoLink
            };
            actorDtos.Add(actorDto);
        }

        return actorDtos;
    }
}
