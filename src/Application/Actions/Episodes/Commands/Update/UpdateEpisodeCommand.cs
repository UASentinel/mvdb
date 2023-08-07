using MediatR;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Episodes.Commands.Update;

public record UpdateEpisodeCommand : IRequest
{
    public int EpisodeId { get; init; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public int Order { get; set; }
    public DateTime ReleaseDate { get; set; }
}