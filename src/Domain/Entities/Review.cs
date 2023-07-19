namespace MvDb.Domain.Entities;
public class Review : BaseAuditableEntity
{
    public byte Rate { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    public DateTime Date { get; set; }
    public int MediaId { get; set; }
    public Media Media { get; set; }
    // Problem with Review-User RelationShip
    public string UserId { get; set; }
    //public User { get; set; }
}
