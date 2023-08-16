using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.Repositories;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;

namespace MvDb.Infrastructure.Repositories;

public class EpisodeRepository : IEpisodeRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public EpisodeRepository(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public ICollection<Episode> Get()
    {
        return _applicationDbContext.Episodes.ToList();
    }

    public async Task<Episode?> GetById(int id)
    {
        return await _applicationDbContext.Episodes.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<bool> Create(Episode episode, CancellationToken cancellationToken)
    {
        await _applicationDbContext.Episodes.AddAsync(episode);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> Update(Episode episode, CancellationToken cancellationToken)
    {
        var dbEpisode = await _applicationDbContext.Episodes.FirstOrDefaultAsync(e => e.Id == episode.Id);
        if (dbEpisode == null)
            return false;

        dbEpisode.Title = episode.Title;
        dbEpisode.Description = episode.Description;
        dbEpisode.Duration = episode.Duration;
        dbEpisode.Order = episode.Order;
        dbEpisode.ReleaseDate = episode.ReleaseDate;

        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        var episode = await _applicationDbContext.Episodes.FirstOrDefaultAsync(e => e.Id == id);
        if (episode == null)
            return false;

        _applicationDbContext.Episodes.Remove(episode);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> UpdateEpisodesOrder(ICollection<Episode> episodes, CancellationToken cancellationToken)
    {
        foreach (var episode in episodes)
        {
            var dbEpisode = await _applicationDbContext.Episodes.FirstOrDefaultAsync(e => e.Id == episode.Id);
            if (dbEpisode == null)
                return false;

            dbEpisode.Order = episode.Order;
        }

        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> RestoreSeasonEpisodesOrder(int seasonId, CancellationToken cancellationToken)
    {
        var episodes = _applicationDbContext.Episodes
            .Where(e => e.SeasonId == seasonId)
            .OrderBy(s => s.Order)
            .ToList();

        for (int i = 0; i < episodes.Count; i++)
            episodes[i].Order = (byte)(i + 1);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
