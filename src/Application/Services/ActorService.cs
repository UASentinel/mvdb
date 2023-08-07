using System.Security.AccessControl;
using MediatR;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Application.Common.Interfaces.Repositories;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;

namespace MvDb.Application.Services;

public class ActorService : IActorService
{
    private readonly IActorRepository _actorRepository;
    private readonly IImageService _imageService;
    public ActorService(IActorRepository actorRepository, IImageService imageService)
    {
        _actorRepository = actorRepository;
        _imageService = imageService;
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

    public async Task<bool> Update(Actor actor, IFormFile photoFile, CancellationToken cancellationToken)
    {
        if (photoFile == null)
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
}
