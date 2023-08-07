using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;

namespace MvDb.Application.Common.Interfaces.Repositories;

public interface IMediaRepository : IRepository<Media>
{
    ICollection<MediaGenre> GetMediaGenres(int mediaId);
    Task<bool> AddGenre(MediaGenre mediaGenre, CancellationToken cancellationToken);
    Task<bool> UpdateGenre(MediaGenre mediaGenre, CancellationToken cancellationToken);
    Task<bool> DeleteGenre(int mediaId, int genreId, CancellationToken cancellationToken);
}
