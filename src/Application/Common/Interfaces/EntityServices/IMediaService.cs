using MvDb.Application.Actions.Medias.Queries.Search;
using MvDb.Domain.Entities;

namespace MvDb.Application.Common.Interfaces.EntityServices;

public interface IMediaService : IPhotoEntityService<Media, SearchMediasQuery>
{
    Task<bool> UpdateGenres(int mediaId, ICollection<MediaGenre> mediaGenres, CancellationToken cancellationToken);
    Task<bool> UpdateDirectors(int mediaId, ICollection<MediaDirector> mediaDirectors, CancellationToken cancellationToken);
    Task<bool> UpdateActors(int mediaId, ICollection<MediaActor> mediaActors, CancellationToken cancellationToken);
    byte CountRating(Media media);
}
