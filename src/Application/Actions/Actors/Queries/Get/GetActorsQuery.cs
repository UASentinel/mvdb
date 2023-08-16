using MediatR;
using MvDb.Application.Actions.Actors.DataTransferObjects;
namespace MvDb.Application.Actions.Actors.Queries.Get;

public record GetActorsQuery : IRequest<ICollection<ActorDto>>;