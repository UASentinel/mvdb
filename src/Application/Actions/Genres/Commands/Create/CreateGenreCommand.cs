using MediatR;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Genres.Commands.Create;

public record CreateGenreCommand : IRequest<int>
{
    public string Name { get; init; }
}