using MediatR;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Episodes.Commands.Create;

public record CreateEpisodeCommand : IRequest<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public int Order { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int SeasonId { get; set; }
}