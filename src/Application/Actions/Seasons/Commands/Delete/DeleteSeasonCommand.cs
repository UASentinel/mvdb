using MediatR;

namespace MvDb.Application.Actions.Seasons.Commands.Delete;

public record DeleteSeasonCommand(int Id) : IRequest;