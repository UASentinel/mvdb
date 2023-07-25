namespace MvDb.Domain.Entities;
public class Season : BaseAuditableEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Order { get; set; }
    public string? PosterLink { get; set; }
    public string? TrailerLink { get; set; }
    public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
    public int MediaId { get; set; }
    public Media Media { get; set; }
}
