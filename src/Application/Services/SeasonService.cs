using System.Security.AccessControl;
using MediatR;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Application.Common.Interfaces.Repositories;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;

namespace MvDb.Application.Services;

public class SeasonService : ISeasonService
{
    private readonly ISeasonRepository _seasonRepository;
    private readonly IImageService _imageService;
    public SeasonService(ISeasonRepository seasonRepository, IImageService imageService)
    {
        _seasonRepository = seasonRepository;
        _imageService = imageService;
    }
    public ICollection<Season> Get()
    {
        return _seasonRepository.Get();
    }

    public async Task<Season?> GetById(int id)
    {
        return await _seasonRepository.GetById(id);
    }

    public async Task<bool> Create(Season season, IFormFile posterFile, CancellationToken cancellationToken)
    {
        await _seasonRepository.Create(season, cancellationToken);

        var mediaId = await GetMediaId(season.Id);
        season.PosterLink = await _imageService.UploadSeasonPoster(posterFile, season.Id, mediaId);

        return await _seasonRepository.Update(season, cancellationToken);
    }

    public async Task<bool> Update(Season season, IFormFile posterFile, CancellationToken cancellationToken)
    {
        if (posterFile == null)
        {
            season.PosterLink = null;
            var mediaId = await GetMediaId(season.Id);
            await _imageService.DeleteSeasonPoster(season.Id, mediaId);
        }
        else
        {
            var mediaId = await GetMediaId(season.Id);
            season.PosterLink = await _imageService.UploadSeasonPoster(posterFile, season.Id, mediaId);
        }

        return await _seasonRepository.Update(season, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        var mediaId = await GetMediaId(id);
        await _imageService.DeleteSeasonPoster(id, mediaId);

        return await _seasonRepository.Delete(id, cancellationToken);
    }

    private async Task<int> GetMediaId(int seasonId)
    {
        var season = await _seasonRepository.GetById(seasonId);

        if (season == null)
            return 0;

        return season.MediaId;
    }
}
