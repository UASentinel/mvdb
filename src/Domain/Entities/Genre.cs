namespace MvDb.Domain.Entities;
public class Genre : BaseAuditableEntity
{
    public string Name { get; set; }
    public List<Media> Media { get; set; } = new List<Media>();
}
