namespace MvDb.Domain.Entities;
public class Director : BaseAuditableEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Biography { get; set; }
    public string? PhotoLink { get; set; }
    public List<Media> Media { get; set; } = new List<Media>();
}