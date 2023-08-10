using System.Security.AccessControl;
using MediatR;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Application.Common.Interfaces.Repositories;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;

namespace MvDb.Application.Services;

public class DirectorService : IDirectorService
{
    private readonly IDirectorRepository _directorRepository;
    private readonly IImageService _imageService;
    public DirectorService(IDirectorRepository directorRepository, IImageService imageService)
    {
        _directorRepository = directorRepository;
        _imageService = imageService;
    }
    public ICollection<Director> Get()
    {
        return _directorRepository.Get();
    }

    public async Task<Director?> GetById(int id)
    {
        return await _directorRepository.GetById(id);
    }

    public async Task<bool> Create(Director director, IFormFile photoFile, CancellationToken cancellationToken)
    {
        await _directorRepository.Create(director, cancellationToken);

        director.PhotoLink = await _imageService.UploadDirectorPhoto(photoFile, director.Id);

        return await _directorRepository.Update(director, cancellationToken);
    }

    public async Task<bool> Update(Director director, IFormFile photoFile, bool deletePhoto, CancellationToken cancellationToken)
    {
        if (deletePhoto)
        {
            director.PhotoLink = null;
            await _imageService.DeleteDirectorPhoto(director.Id);
        }
        else
            director.PhotoLink = await _imageService.UploadDirectorPhoto(photoFile, director.Id);

        return await _directorRepository.Update(director, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        await _imageService.DeleteDirectorPhoto(id);

        return await _directorRepository.Delete(id, cancellationToken);
    }

    public ICollection<Director> Search(object searchPattern)
    {
        throw new NotImplementedException();
    }
}
