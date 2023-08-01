using MediatR;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Genres.Commands.Update;

public record UpdateGenreCommand : IRequest
{
    public int Id { get; init; }
    public string Name { get; init; }
}