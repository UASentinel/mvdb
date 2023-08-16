using MediatR;

namespace MvDb.Application.Actions.Genres.Commands.Delete;

public record DeleteGenreCommand(int Id) : IRequest;