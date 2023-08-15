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

        await _seasonRepository.Update(season, cancellationToken);
        return await _seasonRepository.RestoreMediaSeasonsOrder(season.MediaId, cancellationToken);
    }

    public async Task<bool> Update(Season season, IFormFile posterFile, bool deletePoster, CancellationToken cancellationToken)
    {
        var mediaId = await GetMediaId(season.Id);

        if (deletePoster)
        {
            season.PosterLink = null;
            await _imageService.DeleteSeasonPoster(season.Id, mediaId);
        }
        else
        {
            season.PosterLink = await _imageService.UploadSeasonPoster(posterFile, season.Id, mediaId);
        }

        await _seasonRepository.Update(season, cancellationToken);
        return await _seasonRepository.RestoreMediaSeasonsOrder(mediaId, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        var mediaId = await GetMediaId(id);
        await _imageService.DeleteSeasonPoster(id, mediaId);

        await _seasonRepository.Delete(id, cancellationToken);
        return await _seasonRepository.RestoreMediaSeasonsOrder(mediaId, cancellationToken);
    }

    private async Task<int> GetMediaId(int seasonId)
    {
        var season = await _seasonRepository.GetById(seasonId);

        if (season == null)
            return 0;

        return season.MediaId;
    }

    public async Task<bool> UpdateSeasonsOrder(ICollection<Season> seasons, CancellationToken cancellationToken){
        if (seasons == null || seasons.Count == 0)
            return false;

        await _seasonRepository.UpdateSeasonsOrder(seasons, cancellationToken);

        var mediaId = await GetMediaId(seasons.FirstOrDefault().Id);
        return await _seasonRepository.RestoreMediaSeasonsOrder(mediaId, cancellationToken);
    }

    public ICollection<Season> Search(object searchPattern)
    {
        throw new NotImplementedException();
    }
}
