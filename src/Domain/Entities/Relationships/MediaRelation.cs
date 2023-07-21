namespace MvDb.Domain.Entities;
public class MediaRelation : BaseAuditableEntity
{
    public int MediaId { get; set; }
    public Media Media { get; set; }
    public byte Order { get; set; }
}