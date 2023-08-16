using MediatR;
using MvDb.Application.Actions.Actors.DataTransferObjects;

namespace MvDb.Application.Actions.Actors.Queries.GetById;

public record GetActorByIdQuery(int Id) : IRequest<ActorDto>;