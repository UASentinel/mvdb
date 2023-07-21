namespace MvDb.Domain.Entities;
public class MediaActor : MediaRelation
{
    public int ActorId { get; set; }
    public Actor Actor { get; set; }
}