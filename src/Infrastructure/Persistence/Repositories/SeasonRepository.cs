using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.Repositories;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;

namespace MvDb.Infrastructure.Repositories;

public class SeasonRepository : ISeasonRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public SeasonRepository(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public ICollection<Season> Get()
    {
        return _applicationDbContext.Seasons.ToList();
    }

    public async Task<Season?> GetById(int id)
    {
        var season = await _applicationDbContext.Seasons
            .Include(s => s.Episodes)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (season != null)
            season.Episodes = season.Episodes.OrderBy(e => e.Order).ToList();

        return season;
    }

    public async Task<bool> Create(Season season, CancellationToken cancellationToken)
    {
        await _applicationDbContext.Seasons.AddAsync(season);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> Update(Season season, CancellationToken cancellationToken)
    {
        var dbSeason = await _applicationDbContext.Seasons.FirstOrDefaultAsync(s => s.Id == season.Id);
        if (dbSeason == null)
            return false;

        dbSeason.Title = season.Title;
        dbSeason.Description = season.Description;
        dbSeason.Order = season.Order;
        dbSeason.PosterLink = season.PosterLink;
        dbSeason.TrailerLink = season.TrailerLink;

        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        var season = await _applicationDbContext.Seasons.FirstOrDefaultAsync(s => s.Id == id);
        if (season == null)
            return false;

        _applicationDbContext.Seasons.Remove(season);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> UpdateSeasonsOrder(ICollection<Season> seasons, CancellationToken cancellationToken)
    {
        foreach (var season in seasons)
        {
            var dbSeason = await _applicationDbContext.Seasons.FirstOrDefaultAsync(s => s.Id == season.Id);
            if (dbSeason == null)
                return false;

            dbSeason.Order = season.Order;
        }

        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> RestoreMediaSeasonsOrder(int mediaId, CancellationToken cancellationToken)
    {
        var seasons = _applicationDbContext.Seasons
            .Where(s => s.MediaId == mediaId)
            .OrderBy(s => s.Order)
            .ToList();

        for (int i = 0; i < seasons.Count; i++)
            seasons[i].Order = (byte)(i + 1);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
