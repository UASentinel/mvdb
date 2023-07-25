namespace MvDb.Domain.Entities;
public class Genre : BaseAuditableEntity
{
    public string Name { get; set; }
    public ICollection<MediaGenre> MediaGenres { get; set; } = new List<MediaGenre>();
}
