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
    public ICollection<Genre> Genres { get; set; } = new List<Genre> { };
    public ICollection<Actor> Actors { get; set; } = new List<Actor> { };
    public ICollection<Director> Directors { get; set; } = new List<Director> { };
    public ICollection<Episode> Episodes { get; set; } = new List<Episode> { };
    public ICollection<Review> Reviews { get; set; } = new List<Review> { };
}
