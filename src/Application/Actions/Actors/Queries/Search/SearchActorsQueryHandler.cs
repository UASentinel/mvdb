using MediatR;
using MvDb.Application.Actions.Actors.DataTransferObjects;
using MvDb.Application.Actions.AgeRatings.DataTransferObjects;
using MvDb.Application.Actions.Directors.DataTransferObjects;
using MvDb.Application.Actions.Episodes.DataTransferObjects;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Actions.Medias.DataTransferObjects;
using MvDb.Application.Actions.Seasons.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Actors.Queries.Search;

public class SearchActorsQueryHandler : IRequestHandler<SearchActorsQuery, ICollection<ActorDto>>
{
    private readonly IActorService _actorService;

    public SearchActorsQueryHandler(IActorService actorService)
    {
        _actorService = actorService;
    }

    public async Task<ICollection<ActorDto>> Handle(SearchActorsQuery request, CancellationToken cancellationToken)
    {
        var actors = _actorService.Search(request);

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
