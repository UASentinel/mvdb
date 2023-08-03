using MediatR;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Episodes.Commands.Delete;

public record DeleteEpisodeCommand(int Id) : IRequest;