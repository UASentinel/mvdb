using MediatR;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Actors.Commands.Delete;

public record DeleteActorCommand(int Id) : IRequest;