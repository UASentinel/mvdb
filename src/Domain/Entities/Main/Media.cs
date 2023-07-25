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
    public int Duration { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public ICollection<MediaGenre> MediaGenres { get; set; } = new List<MediaGenre>();
    public ICollection<MediaActor> MediaActors { get; set; } = new List<MediaActor>();
    public ICollection<MediaDirector> MediaDirectors { get; set; } = new List<MediaDirector>();
    public ICollection<Season> Seasons { get; set; } = new List<Season> { };
    public ICollection<Review> Reviews { get; set; } = new List<Review> { };
}
