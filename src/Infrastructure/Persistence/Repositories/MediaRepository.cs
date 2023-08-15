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
        var medias = _applicationDbContext.Medias
            .Include(m => m.AgeRating)
            .Include(m => m.Reviews)
            .Include(m => m.MediaGenres)
            .ThenInclude(m => m.Genre)
            .Include(m => m.Seasons)
            .ThenInclude(s => s.Episodes)
            .ToList();

        foreach(var media in medias)
            media.MediaGenres = media.MediaGenres.OrderBy(m => m.Order).ToList();

        return medias;
    }

    public async Task<Media?> GetById(int id)
    {
        var media = await _applicationDbContext.Medias
            .Include(m => m.AgeRating)
            .Include(m => m.Reviews)
            .Include(m => m.MediaActors)
            .ThenInclude(m => m.Actor)
            .Include(m => m.MediaDirectors)
            .ThenInclude(m => m.Director)
            .Include(m => m.MediaGenres)
            .ThenInclude(m => m.Genre)
            .Include(m => m.Seasons)
            .ThenInclude(s => s.Episodes)
            .FirstOrDefaultAsync(m => m.Id == id);

        if(media != null)
        {
            media.MediaGenres = media.MediaGenres.OrderBy(m => m.Order).ToList();
            media.MediaActors = media.MediaActors.OrderBy(m => m.Order).ToList();
            media.MediaDirectors = media.MediaDirectors.OrderBy(m => m.Order).ToList();
            media.Seasons = media.Seasons.OrderBy(s => s.Order).ToList();
        }

        return media;
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

    public async Task<bool> RestoreGenresOrder(int mediaId, CancellationToken cancellationToken)
    {
        var genres = _applicationDbContext.MediaGenres
            .Where(m => m.MediaId == mediaId)
            .OrderBy(m => m.Order)
            .ToList();

        for (int i = 0; i < genres.Count; i++)
            genres[i].Order = (byte)(i + 1);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public ICollection<MediaDirector> GetMediaDirectors(int mediaId)
    {
        return _applicationDbContext.MediaDirectors.Include(m => m.Director).Where(m => m.MediaId == mediaId).ToList();
    }

    public async Task<bool> AddDirector(MediaDirector mediaDirector, CancellationToken cancellationToken)
    {
        _applicationDbContext.MediaDirectors.Add(mediaDirector);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> UpdateDirector(MediaDirector mediaDirector, CancellationToken cancellationToken)
    {
        var dbMediaDirector = await _applicationDbContext.MediaDirectors.FirstOrDefaultAsync(m => m.MediaId == mediaDirector.MediaId && m.DirectorId == mediaDirector.DirectorId);
        if (dbMediaDirector == null)
            return false;

        dbMediaDirector.MediaId = mediaDirector.MediaId;
        dbMediaDirector.DirectorId = mediaDirector.DirectorId;
        dbMediaDirector.Order = mediaDirector.Order;

        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> DeleteDirector(int mediaId, int directorId, CancellationToken cancellationToken)
    {
        var mediaDirector = _applicationDbContext.MediaDirectors.FirstOrDefault(m => m.MediaId == mediaId && m.DirectorId == directorId);
        if (mediaDirector == null)
            return false;

        _applicationDbContext.MediaDirectors.Remove(mediaDirector);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> RestoreDirectorsOrder(int mediaId, CancellationToken cancellationToken)
    {
        var directors = _applicationDbContext.MediaDirectors
            .Where(m => m.MediaId == mediaId)
            .OrderBy(m => m.Order)
            .ToList();

        for (int i = 0; i < directors.Count; i++)
            directors[i].Order = (byte)(i + 1);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public ICollection<MediaActor> GetMediaActors(int mediaId)
    {
        return _applicationDbContext.MediaActors.Include(m => m.Actor).Where(m => m.MediaId == mediaId).ToList();
    }

    public async Task<bool> AddActor(MediaActor mediaActor, CancellationToken cancellationToken)
    {
        _applicationDbContext.MediaActors.Add(mediaActor);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> UpdateActor(MediaActor mediaActor, CancellationToken cancellationToken)
    {
        var dbMediaActor = await _applicationDbContext.MediaActors.FirstOrDefaultAsync(m => m.MediaId == mediaActor.MediaId && m.ActorId == mediaActor.ActorId);
        if (dbMediaActor == null)
            return false;

        dbMediaActor.MediaId = mediaActor.MediaId;
        dbMediaActor.ActorId = mediaActor.ActorId;
        dbMediaActor.Order = mediaActor.Order;

        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> DeleteActor(int mediaId, int actorId, CancellationToken cancellationToken)
    {
        var mediaActor = _applicationDbContext.MediaActors.FirstOrDefault(m => m.MediaId == mediaId && m.ActorId == actorId);
        if (mediaActor == null)
            return false;

        _applicationDbContext.MediaActors.Remove(mediaActor);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> RestoreActorsOrder(int mediaId, CancellationToken cancellationToken)
    {
        var actors = _applicationDbContext.MediaActors
            .Where(m => m.MediaId == mediaId)
            .OrderBy(m => m.Order)
            .ToList();

        for (int i = 0; i < actors.Count; i++)
            actors[i].Order = (byte)(i + 1);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
