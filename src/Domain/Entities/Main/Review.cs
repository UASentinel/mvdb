using System.Net;

namespace MvDb.Domain.Entities;
public class Review : BaseAuditableEntity
{
    public byte Rate { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    public DateTime Date { get; set; }
    public int MediaId { get; set; }
    public Media Media { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}
