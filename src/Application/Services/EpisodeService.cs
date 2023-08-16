using System.Security.AccessControl;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Application.Common.Interfaces.Repositories;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;

namespace MvDb.Application.Services;

public class EpisodeService : IEpisodeService
{
    private readonly IEpisodeRepository _episodeRepository;
    public EpisodeService(IEpisodeRepository episodeRepository)
    {
        _episodeRepository = episodeRepository;
    }
    public ICollection<Episode> Get()
    {
        return _episodeRepository.Get();
    }

    public async Task<Episode?> GetById(int id)
    {
        return await _episodeRepository.GetById(id);
    }

    public async Task<bool> Create(Episode episode, CancellationToken cancellationToken)
    {
        await _episodeRepository.Create(episode, cancellationToken);
        return await _episodeRepository.RestoreSeasonEpisodesOrder(episode.SeasonId, cancellationToken);
    }

    public async Task<bool> Update(Episode episode, CancellationToken cancellationToken)
    {
        await _episodeRepository.Update(episode, cancellationToken);

        var seasonId = await GetSeasonId(episode.Id);
        return await _episodeRepository.RestoreSeasonEpisodesOrder(seasonId, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        await _episodeRepository.Delete(id, cancellationToken);

        var seasonId = await GetSeasonId(id);
        return await _episodeRepository.RestoreSeasonEpisodesOrder(seasonId, cancellationToken);
    }

    public ICollection<Episode> Search(object searchPattern)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateEpisodesOrder(ICollection<Episode> episodes, CancellationToken cancellationToken)
    {
        if (episodes == null || episodes.Count == 0)
            return false;

        await _episodeRepository.UpdateEpisodesOrder(episodes, cancellationToken);

        var seasonId = await GetSeasonId(episodes.FirstOrDefault().Id);
        return await _episodeRepository.RestoreSeasonEpisodesOrder(seasonId, cancellationToken);
    }

    private async Task<int> GetSeasonId(int episodeId)
    {
        var episode = await _episodeRepository.GetById(episodeId);

        if (episode == null)
            return 0;

        return episode.SeasonId;
    }
}
