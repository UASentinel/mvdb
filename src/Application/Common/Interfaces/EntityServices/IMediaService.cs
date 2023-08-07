using System.Data;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Actions.Medias.Queries.Search;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;

namespace MvDb.Application.Common.Interfaces.EntityServices;

public interface IMediaService : IPhotoEntityService<Media, SearchMediasQuery>
{
    Task<bool> UpdateGenres(int mediaId, ICollection<MediaGenre> mediaGenres, CancellationToken cancellationToken);
    byte CountRating(Media media);
}
