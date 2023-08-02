using System.Drawing;

namespace MvDb.Domain.Entities;
public class AgeRating : BaseAuditableEntity
{
    public string Name { get; set; }
    public byte MinAge { get; set; }
    public ICollection<Media> Media { get; set; } = new List<Media>();
    public AgeRating() { }
    public AgeRating(string name, byte minAge)
    {
        Name = name;
        MinAge = minAge;
    }
    public AgeRating(int id, string name, byte minAge) : this(name, minAge)
    {
        Id = id;
    }
}
