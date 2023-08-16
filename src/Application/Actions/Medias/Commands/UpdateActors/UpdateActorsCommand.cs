using MediatR;
using MvDb.Application.Actions.Medias.DataTransferObjects.Relationships;

namespace MvDb.Application.Actions.Medias.Commands.UpdateActors;

public record UpdateActorsCommand : IRequest
{
    public int MediaId { get; set; }
    public ICollection<MediaActorDto> MediaActorDtos { get; set; } = new List<MediaActorDto>();
}