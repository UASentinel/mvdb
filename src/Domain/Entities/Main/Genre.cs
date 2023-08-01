namespace MvDb.Domain.Entities;
public class Genre : BaseAuditableEntity
{
    public string Name { get; set; }
    public ICollection<MediaGenre> MediaGenres { get; set; } = new List<MediaGenre>();
    public Genre() { }
    public Genre(string name)
    {
        Name = name;
    }
    public Genre(int id, string name) : this(name)
    {
        Id = id;
    }
}
