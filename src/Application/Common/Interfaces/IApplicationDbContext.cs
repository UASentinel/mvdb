using Microsoft.EntityFrameworkCore;
using MvDb.Domain.Entities;

namespace MvDb.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    public DbSet<Actor> Actors { get; }
    public DbSet<MediaActor> MediaActors { get; }
    public DbSet<AgeRating> AgeRatings { get; }
    public DbSet<ApplicationUser> ApplicationUsers { get; }
    public DbSet<Director> Directors { get; }
    public DbSet<MediaDirector> MediaDirectors { get; }
    public DbSet<Episode> Episodes { get; }
    public DbSet<Genre> Genres { get; }
    public DbSet<MediaGenre> MediaGenres { get; }
    public DbSet<Media> Medias { get; }
    public DbSet<Review> Reviews { get; }
    public DbSet<Season> Seasons { get; }
}
