namespace MvDb.Domain.Entities;
public class AgeRating : BaseAuditableEntity
{
    public string Name { get; set; }
    public byte MinAge { get; set; }
    public List<Media> Media { get; set; } = new List<Media>();
}
