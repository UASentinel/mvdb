using MediatR;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Genres.Commands.Update;

public record UpdateGenreCommand : IRequest
{
    public int GenreId { get; init; }
    public string Name { get; init; }
}