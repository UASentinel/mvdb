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
        return _applicationDbContext.Medias.ToList();
    }

    public async Task<Media?> GetById(int id)
    {
        return await _applicationDbContext.Medias.FirstOrDefaultAsync(m => m.Id == id);
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

        _applicationDbContext.Medias.Entry(dbMedia).CurrentValues.SetValues(media);
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
}
