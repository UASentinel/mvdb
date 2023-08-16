using MediatR;

namespace MvDb.Application.Actions.Medias.Commands.Delete;

public record DeleteMediaCommand(int Id) : IRequest;