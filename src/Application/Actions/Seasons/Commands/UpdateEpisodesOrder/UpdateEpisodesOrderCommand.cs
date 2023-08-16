using MediatR;
using MvDb.Application.Actions.Seasons.DataTransferObjects;

namespace MvDb.Application.Actions.Seasons.Commands.UpdateEpisodesOrder;

public record UpdateEpisodesOrderCommand : IRequest
{
    public int SeasonId { get; set; }
    public ICollection<SeasonEpisodeDto> SeasonEpisodeDtos { get; set; } = new List<SeasonEpisodeDto>();
}