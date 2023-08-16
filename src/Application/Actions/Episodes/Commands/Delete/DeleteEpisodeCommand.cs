using MediatR;

namespace MvDb.Application.Actions.Episodes.Commands.Delete;

public record DeleteEpisodeCommand(int Id) : IRequest;