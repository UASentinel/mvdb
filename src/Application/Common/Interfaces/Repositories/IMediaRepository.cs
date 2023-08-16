using MvDb.Domain.Entities;

namespace MvDb.Application.Common.Interfaces.Repositories;

public interface IMediaRepository : IRepository<Media>
{
    ICollection<MediaGenre> GetMediaGenres(int mediaId);
    ICollection<MediaDirector> GetMediaDirectors(int mediaId);
    ICollection<MediaActor> GetMediaActors(int mediaId);
    Task<bool> AddGenre(MediaGenre mediaGenre, CancellationToken cancellationToken);
    Task<bool> UpdateGenre(MediaGenre mediaGenre, CancellationToken cancellationToken);
    Task<bool> DeleteGenre(int mediaId, int genreId, CancellationToken cancellationToken);
    Task<bool> RestoreGenresOrder(int mediaId, CancellationToken cancellationToken);
    Task<bool> AddDirector(MediaDirector mediaDirector, CancellationToken cancellationToken);
    Task<bool> UpdateDirector(MediaDirector mediaDirector, CancellationToken cancellationToken);
    Task<bool> DeleteDirector(int mediaId, int directorId, CancellationToken cancellationToken);
    Task<bool> RestoreDirectorsOrder(int mediaId, CancellationToken cancellationToken);
    Task<bool> AddActor(MediaActor mediaActor, CancellationToken cancellationToken);
    Task<bool> UpdateActor(MediaActor mediaActor, CancellationToken cancellationToken);
    Task<bool> DeleteActor(int mediaId, int actorId, CancellationToken cancellationToken);
    Task<bool> RestoreActorsOrder(int mediaId, CancellationToken cancellationToken);
}
