using Microsoft.EntityFrameworkCore;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.Repositories;
using MvDb.Domain.Entities;

namespace MvDb.Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GenreRepository(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public ICollection<Genre> Get()
    {
        return _applicationDbContext.Genres.ToList();
    }

    public async Task<Genre?> GetById(int id)
    {
        return await _applicationDbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<bool> Create(Genre genre, CancellationToken cancellationToken)
    {
        await _applicationDbContext.Genres.AddAsync(genre);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> Update(Genre genre, CancellationToken cancellationToken)
    {
        var dbGenre = await _applicationDbContext.Genres.FirstOrDefaultAsync(g => g.Id == genre.Id);
        if (dbGenre == null)
            return false;

        dbGenre.Name = genre.Name;

        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        var genre = await _applicationDbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);
        if (genre == null)
            return false;

        _applicationDbContext.Genres.Remove(genre);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }
}
