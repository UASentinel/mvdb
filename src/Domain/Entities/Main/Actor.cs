namespace MvDb.Domain.Entities;
public class Actor : BaseAuditableEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Biography { get; set; }
    public string? PhotoLink { get; set; }
    public ICollection<MediaActor> MediaActors { get; set; } = new List<MediaActor>();
}