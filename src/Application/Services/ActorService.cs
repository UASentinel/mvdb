using Microsoft.AspNetCore.Http;
using MvDb.Application.Actions.Actors.Queries.Search;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Application.Common.Interfaces.Repositories;
using MvDb.Domain.Entities;

namespace MvDb.Application.Services;

public class ActorService : IActorService
{
    private readonly IActorRepository _actorRepository;
    private readonly IImageService _imageService;
    private readonly ISearchService _searchService;
    public ActorService(IActorRepository actorRepository, IImageService imageService, ISearchService searchService)
    {
        _actorRepository = actorRepository;
        _imageService = imageService;
        _searchService = searchService;
    }
    public ICollection<Actor> Get()
    {
        return _actorRepository.Get();
    }

    public async Task<Actor?> GetById(int id)
    {
        return await _actorRepository.GetById(id);
    }

    public async Task<bool> Create(Actor actor, IFormFile photoFile, CancellationToken cancellationToken)
    {
        await _actorRepository.Create(actor, cancellationToken);

        actor.PhotoLink = await _imageService.UploadActorPhoto(photoFile, actor.Id);

        return await _actorRepository.Update(actor, cancellationToken);
    }

    public async Task<bool> Update(Actor actor, IFormFile photoFile, bool deletePhoto, CancellationToken cancellationToken)
    {
        if (deletePhoto)
        {
            actor.PhotoLink = null;
            await _imageService.DeleteActorPhoto(actor.Id);
        }
        else
            actor.PhotoLink = await _imageService.UploadActorPhoto(photoFile, actor.Id);

        return await _actorRepository.Update(actor, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        await _imageService.DeleteActorPhoto(id);

        return await _actorRepository.Delete(id, cancellationToken);
    }

    public ICollection<Actor> Search(SearchActorsQuery searchPattern)
    {
        var directors = _actorRepository.Get();

        return directors.Where(d => FilterActor(d, searchPattern)).ToList();
    }

    private bool FilterActor(Actor actor, SearchActorsQuery searchPattern)
    {
        if (searchPattern.Name != null && searchPattern.Name != String.Empty)
        {
            var flag = _searchService.CheckKeyWords(actor.FirstName, searchPattern.Name);
            if (!flag)
                flag = flag || _searchService.CheckKeyWords(actor.LastName, searchPattern.Name);

            if (!flag)
                return flag;
        }

        return true;
    }
}
