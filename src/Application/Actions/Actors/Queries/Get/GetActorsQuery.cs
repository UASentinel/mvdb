using MediatR;
using MvDb.Application.Actions.Actors.DataTransferObjects;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
namespace MvDb.Application.Actions.Actors.Queries.Get;

public record GetActorsQuery : IRequest<ICollection<ActorDto>>;