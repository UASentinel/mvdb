﻿using System.Security.AccessControl;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Application.Common.Interfaces.Repositories;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;

namespace MvDb.Application.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;
    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }
    public ICollection<Genre> Get()
    {
        return _genreRepository.Get();
    }

    public async Task<Genre?> GetById(int id)
    {
        return await _genreRepository.GetById(id);
    }

    public async Task<bool> Create(Genre entity, CancellationToken cancellationToken)
    {
        return await _genreRepository.Create(entity, cancellationToken);
    }

    public async Task<bool> Update(Genre entity, CancellationToken cancellationToken)
    {
        return await _genreRepository.Update(entity, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        return await _genreRepository.Delete(id, cancellationToken);
    }
}
