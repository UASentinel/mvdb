using System.Drawing;

namespace MvDb.Domain.Entities;
public class AgeRating : BaseAuditableEntity
{
    public string Name { get; set; }
    public byte MinAge { get; set; }
    public ICollection<Media> Media { get; set; } = new List<Media>();
}
