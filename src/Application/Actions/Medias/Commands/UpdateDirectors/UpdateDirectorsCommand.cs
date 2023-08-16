using MediatR;
using MvDb.Application.Actions.Medias.DataTransferObjects.Relationships;

namespace MvDb.Application.Actions.Medias.Commands.UpdateDirectors;

public record UpdateDirectorsCommand : IRequest
{
    public int MediaId { get; set; }
    public ICollection<MediaDirectorDto> MediaDirectorDtos { get; set; } = new List<MediaDirectorDto>();
}