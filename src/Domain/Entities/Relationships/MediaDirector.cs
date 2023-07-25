namespace MvDb.Domain.Entities;
public class MediaDirector : MediaRelation
{
    public int DirectorId { get; set; }
    public Director Director { get; set; }
}