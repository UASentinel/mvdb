using MvDb.Domain.Enums;

namespace MvDb.Domain.Entities;
public class Media : BaseAuditableEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public MediaType MediaType { get; set; }
    public string? PosterLink { get; set; }
    public string? TrailerLink { get; set; }
    public int AgeRatingId { get; set; }
    public AgeRating AgeRating { get; set; }
    public List<Genre> Genres { get; set; } = new List<Genre> { };
    public List<Actor> Actors { get; set; } = new List<Actor> { };
    public List<Director> Directors { get; set; } = new List<Director> { };
    public List<Episode> Episodes { get; set; } = new List<Episode> { };
    public List<Review> Reviews { get; set; } = new List<Review> { };
}
