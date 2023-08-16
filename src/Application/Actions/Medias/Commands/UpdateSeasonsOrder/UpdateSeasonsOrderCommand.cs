using MediatR;
using MvDb.Application.Actions.Medias.DataTransferObjects.Relationships;

namespace MvDb.Application.Actions.Medias.Commands.UpdateSeasonsOrder;

public record UpdateSeasonsOrderCommand : IRequest
{
    public int MediaId { get; set; }
    public ICollection<MediaSeasonDto> MediaSeasonDtos { get; set; } = new List<MediaSeasonDto>();
}