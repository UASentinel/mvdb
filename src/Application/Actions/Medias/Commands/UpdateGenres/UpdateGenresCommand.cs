using MediatR;
using MvDb.Application.Actions.Medias.DataTransferObjects.Relationships;

namespace MvDb.Application.Actions.Medias.Commands.AddGenres;

public record UpdateGenresCommand : IRequest
{
    public int MediaId { get; set; }
    public ICollection<MediaGenreDto> MediaGenreDtos { get; set; } = new List<MediaGenreDto>();
}