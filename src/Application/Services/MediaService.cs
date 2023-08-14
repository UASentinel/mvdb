using System.Security.AccessControl;
using MediatR;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Actions.Directors.Queries.Search;
using MvDb.Application.Actions.Medias.DataTransferObjects;
using MvDb.Application.Actions.Medias.Queries.Search;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Application.Common.Interfaces.Repositories;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;
using MvDb.Domain.Enums;

namespace MvDb.Application.Services;

public class MediaService : IMediaService
{
    private readonly IMediaRepository _mediaRepository;
    private readonly IImageService _imageService;
    private readonly ISearchService _searchService;
    public MediaService(IMediaRepository mediaRepository, IImageService imageService, ISearchService searchService)
    {
        _mediaRepository = mediaRepository;
        _imageService = imageService;
        _searchService = searchService;
    }
    public ICollection<Media> Get()
    {
        return _mediaRepository.Get();
    }

    public async Task<Media?> GetById(int id)
    {
        return await _mediaRepository.GetById(id);
    }

    public async Task<bool> Create(Media media, IFormFile posterFile, CancellationToken cancellationToken)
    {
        await _mediaRepository.Create(media, cancellationToken);

        media.PosterLink = await _imageService.UploadMediaPoster(posterFile, media.Id);

        return await _mediaRepository.Update(media, cancellationToken);
    }

    public async Task<bool> Update(Media media, IFormFile posterFile, bool deletePoster, CancellationToken cancellationToken)
    {
        if (deletePoster)
        {
            media.PosterLink = null;
            await _imageService.DeleteMediaPoster(media.Id);
        }
        else if (posterFile != null)
            media.PosterLink = await _imageService.UploadMediaPoster(posterFile, media.Id);
        else
        {
            var dbMedia = await _mediaRepository.GetById(media.Id);
            if (dbMedia != null)
                media.PosterLink = dbMedia.PosterLink;
        }

        return await _mediaRepository.Update(media, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        await _imageService.DeleteMediaPoster(id);

        return await _mediaRepository.Delete(id, cancellationToken);
    }

    public async Task<bool> UpdateGenres(int mediaId, ICollection<MediaGenre> mediaGenres, CancellationToken cancellationToken)
    {
        var dbMediaGenres = _mediaRepository.GetMediaGenres(mediaId);
        foreach (var mediaGenre in mediaGenres)
        {
            if(mediaGenres.Where(m => m.GenreId == mediaGenre.GenreId).Count() > 1)
            {
                mediaGenres.Remove(mediaGenre);
                continue;
            }

            if (dbMediaGenres.FirstOrDefault(m => m.GenreId == mediaGenre.GenreId) != null)
                await _mediaRepository.UpdateGenre(mediaGenre, cancellationToken);
            else
                await _mediaRepository.AddGenre(mediaGenre, cancellationToken);
        }

        foreach (var dbMediaGenre in dbMediaGenres)
        {
            if (mediaGenres.FirstOrDefault(m => m.GenreId == dbMediaGenre.GenreId) == null)
                await _mediaRepository.DeleteGenre(dbMediaGenre.MediaId, dbMediaGenre.GenreId, cancellationToken);
        }

        await _mediaRepository.RestoreGenresOrder(mediaId, cancellationToken);

        return true;
    }

    public async Task<bool> UpdateDirectors(int mediaId, ICollection<MediaDirector> mediaDirectors, CancellationToken cancellationToken)
    {
        var dbMediaDirectors = _mediaRepository.GetMediaDirectors(mediaId);
        foreach (var mediaDirector in mediaDirectors)
        {
            if (mediaDirectors.Where(m => m.DirectorId == mediaDirector.DirectorId).Count() > 1)
            {
                mediaDirectors.Remove(mediaDirector);
                continue;
            }

            if (dbMediaDirectors.FirstOrDefault(m => m.DirectorId == mediaDirector.DirectorId) != null)
                await _mediaRepository.UpdateDirector(mediaDirector, cancellationToken);
            else
                await _mediaRepository.AddDirector(mediaDirector, cancellationToken);
        }

        foreach (var dbMediaDirector in dbMediaDirectors)
        {
            if (mediaDirectors.FirstOrDefault(m => m.DirectorId == dbMediaDirector.DirectorId) == null)
                await _mediaRepository.DeleteDirector(dbMediaDirector.MediaId, dbMediaDirector.DirectorId, cancellationToken);
        }

        await _mediaRepository.RestoreGenresOrder(mediaId, cancellationToken);

        return true;
    }

    public async Task<bool> UpdateActors(int mediaId, ICollection<MediaActor> mediaActors, CancellationToken cancellationToken)
    {
        var dbMediaActors = _mediaRepository.GetMediaActors(mediaId);
        foreach (var mediaActor in mediaActors)
        {
            if (mediaActors.Where(m => m.ActorId == mediaActor.ActorId).Count() > 1)
            {
                mediaActors.Remove(mediaActor);
                continue;
            }

            if (dbMediaActors.FirstOrDefault(m => m.ActorId == mediaActor.ActorId) != null)
                await _mediaRepository.UpdateActor(mediaActor, cancellationToken);
            else
                await _mediaRepository.AddActor(mediaActor, cancellationToken);
        }

        foreach (var dbMediaActor in dbMediaActors)
        {
            if (mediaActors.FirstOrDefault(m => m.ActorId == dbMediaActor.ActorId) == null)
                await _mediaRepository.DeleteActor(dbMediaActor.MediaId, dbMediaActor.ActorId, cancellationToken);
        }

        await _mediaRepository.RestoreGenresOrder(mediaId, cancellationToken);

        return true;
    }

    public ICollection<Media> Search(SearchMediasQuery searchPattern)
    {
        var medias = _mediaRepository.Get();

        return medias.Where(m => FilterMedia(m, searchPattern)).ToList();
    }

    public byte CountRating(Media media)
    {
        var rating = 0;

        var reviewList = media.Reviews.Select(r => r.Rate).ToList();
        if (reviewList.Count() != 0)
            rating = reviewList.Sum(r => r) / reviewList.Count();

        return (byte)rating;
    }

    private bool FilterMedia(Media media, SearchMediasQuery searchPattern)
    {
        if(searchPattern.Title != null && searchPattern.Title != String.Empty)
        {
            var flag = _searchService.CheckKeyWords(media.Title, searchPattern.Title);

            if (!flag)
                return flag;
        }

        if(searchPattern.MediaType != MediaType.None && media.MediaType != searchPattern.MediaType)
            return false;

        return true;
    }
}
