using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.Repositories;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;

namespace MvDb.Infrastructure.Repositories;

public class MediaRepository : IMediaRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public MediaRepository(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public ICollection<Media> Get()
    {
        return _applicationDbContext.Medias.Include(m => m.AgeRating).Include(m => m.Reviews).ToList();
    }

    public async Task<Media?> GetById(int id)
    {
        return await _applicationDbContext.Medias.Include(m => m.AgeRating).Include(m => m.Reviews).FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<bool> Create(Media media, CancellationToken cancellationToken)
    {
        await _applicationDbContext.Medias.AddAsync(media);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> Update(Media media, CancellationToken cancellationToken)
    {
        var dbMedia = await _applicationDbContext.Medias.FirstOrDefaultAsync(m => m.Id == media.Id);
        if (dbMedia == null)
            return false;

        dbMedia.Title = media.Title;
        dbMedia.Description = media.Description;
        dbMedia.MediaType = media.MediaType;
        dbMedia.PosterLink = media.PosterLink;
        dbMedia.TrailerLink = media.TrailerLink;
        dbMedia.AgeRatingId = media.AgeRatingId;
        dbMedia.Duration = media.Duration;
        dbMedia.ReleaseDate = media.ReleaseDate;

        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        var media = await _applicationDbContext.Medias.FirstOrDefaultAsync(m => m.Id == id);
        if (media == null)
            return false;

        _applicationDbContext.Medias.Remove(media);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public ICollection<MediaGenre> GetMediaGenres(int mediaId)
    {
        return _applicationDbContext.MediaGenres.Include(m => m.Genre).Where(m => m.MediaId == mediaId).ToList();
    }

    public async Task<bool> AddGenre(MediaGenre mediaGenre, CancellationToken cancellationToken)
    {
        _applicationDbContext.MediaGenres.Add(mediaGenre);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> UpdateGenre(MediaGenre mediaGenre, CancellationToken cancellationToken)
    {
        var dbMediaGenre = await _applicationDbContext.MediaGenres.FirstOrDefaultAsync(m => m.MediaId == mediaGenre.MediaId && m.GenreId == mediaGenre.GenreId);
        if (dbMediaGenre == null)
            return false;

        dbMediaGenre.MediaId = mediaGenre.MediaId;
        dbMediaGenre.GenreId = mediaGenre.GenreId;
        dbMediaGenre.Order = mediaGenre.Order;

        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> DeleteGenre(int mediaId, int genreId, CancellationToken cancellationToken)
    {
        var mediaGenre = _applicationDbContext.MediaGenres.FirstOrDefault(m => m.MediaId == mediaId && m.GenreId == genreId);
        if (mediaGenre == null)
            return false;

        _applicationDbContext.MediaGenres.Remove(mediaGenre);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }
}
