using MediatR;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Medias.Commands.Delete;

public record DeleteMediaCommand(int Id) : IRequest;