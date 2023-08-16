namespace MvDb.Application.Actions.Actors.DataTransferObjects;
public class ActorDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Biography { get; set; }
    public string? PhotoLink { get; set; }
}
