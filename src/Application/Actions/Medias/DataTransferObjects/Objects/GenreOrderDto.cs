using MvDb.Application.Actions.Genres.DataTransferObjects;

namespace MvDb.Application.Actions.Medias.DataTransferObjects.Objects;
public class GenreOrderDto : GenreDto
{
    public byte Order { get; set; }
}
