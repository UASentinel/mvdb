using MediatR;
using MvDb.Application.Actions.Actors.DataTransferObjects;

namespace MvDb.Application.Actions.Actors.Queries.Search;

public record SearchActorsQuery : IRequest<ICollection<ActorDto>>
{
    public string Name { get; set; }
}