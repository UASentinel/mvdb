using System.Security.AccessControl;
using MediatR;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Application.Common.Interfaces.Repositories;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;

namespace MvDb.Application.Services;

public class MediaService : IMediaService
{
    private readonly IMediaRepository _mediaRepository;
    private readonly IImageService _imageService;
    public MediaService(IMediaRepository mediaRepository, IImageService imageService)
    {
        _mediaRepository = mediaRepository;
        _imageService = imageService;
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

    public async Task<bool> Update(Media media, IFormFile posterFile, CancellationToken cancellationToken)
    {
        if (posterFile == null)
        {
            media.PosterLink = null;
            await _imageService.DeleteMediaPoster(media.Id);
        }
        else
            media.PosterLink = await _imageService.UploadMediaPoster(posterFile, media.Id);

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

        return true;
    }
}
