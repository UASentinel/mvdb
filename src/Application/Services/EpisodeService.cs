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
        return await _episodeRepository.Create(episode, cancellationToken);
    }

    public async Task<bool> Update(Episode episode, CancellationToken cancellationToken)
    {
        return await _episodeRepository.Update(episode, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        return await _episodeRepository.Delete(id, cancellationToken);
    }

    public ICollection<Episode> Search(object searchPattern)
    {
        throw new NotImplementedException();
    }
}
