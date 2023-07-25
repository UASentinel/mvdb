namespace MvDb.Domain.Entities;
public class MediaGenre : MediaRelation
{
    public int GenreId { get; set; }
    public Genre Genre { get; set; }
}