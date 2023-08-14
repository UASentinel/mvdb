using System.Security.AccessControl;
using MediatR;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Actions.Actors.Queries.Search;
using MvDb.Application.Actions.Directors.Queries.Search;
using MvDb.Application.Actions.Medias.Queries.Search;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Application.Common.Interfaces.Repositories;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;
using MvDb.Domain.Enums;

namespace MvDb.Application.Services;

public class DirectorService : IDirectorService
{
    private readonly IDirectorRepository _directorRepository;
    private readonly IImageService _imageService;
    private readonly ISearchService _searchService;
    public DirectorService(IDirectorRepository directorRepository, IImageService imageService, ISearchService searchService)
    {
        _directorRepository = directorRepository;
        _imageService = imageService;
        _searchService = searchService;
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

    public ICollection<Director> Search(SearchDirectorsQuery searchPattern)
    {
        var directors = _directorRepository.Get();

        return directors.Where(d => FilterDirector(d, searchPattern)).ToList();
    }

    private bool FilterDirector(Director director, SearchDirectorsQuery searchPattern)
    {
        if (searchPattern.Name != null && searchPattern.Name != String.Empty)
        {
            var flag = _searchService.CheckKeyWords(director.FirstName, searchPattern.Name);
            if (!flag)
                flag = flag || _searchService.CheckKeyWords(director.LastName, searchPattern.Name);

            if (!flag)
                return flag;
        }

        return true;
    }
}
