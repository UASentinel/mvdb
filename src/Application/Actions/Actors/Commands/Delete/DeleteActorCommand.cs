using MediatR;

namespace MvDb.Application.Actions.Actors.Commands.Delete;

public record DeleteActorCommand(int Id) : IRequest;