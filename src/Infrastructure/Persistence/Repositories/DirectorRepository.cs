using Microsoft.EntityFrameworkCore;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.Repositories;
using MvDb.Domain.Entities;

namespace MvDb.Infrastructure.Repositories;

public class DirectorRepository : IDirectorRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public DirectorRepository(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public ICollection<Director> Get()
    {
        return _applicationDbContext.Directors.ToList();
    }

    public async Task<Director?> GetById(int id)
    {
        return await _applicationDbContext.Directors.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<bool> Create(Director director, CancellationToken cancellationToken)
    {
        await _applicationDbContext.Directors.AddAsync(director);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> Update(Director director, CancellationToken cancellationToken)
    {
        var dbDirector = await _applicationDbContext.Directors.FirstOrDefaultAsync(d => d.Id == director.Id);
        if (dbDirector == null)
            return false;

        dbDirector.FirstName = director.FirstName;
        dbDirector.LastName = director.LastName;
        dbDirector.DateOfBirth = director.DateOfBirth;
        dbDirector.Biography = director.Biography;
        dbDirector.PhotoLink = director.PhotoLink;

        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        var director = await _applicationDbContext.Directors.FirstOrDefaultAsync(d => d.Id == id);
        if (director == null)
            return false;

        _applicationDbContext.Directors.Remove(director);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }
}
