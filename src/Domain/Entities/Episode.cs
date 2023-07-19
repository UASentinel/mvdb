namespace MvDb.Domain.Entities;
public class Episode : BaseAuditableEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Duration { get; set; }
    public DateTime ReleaseDate { get; set; }
}
