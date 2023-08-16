using Microsoft.EntityFrameworkCore;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.Repositories;
using MvDb.Domain.Entities;

namespace MvDb.Infrastructure.Repositories;

public class ActorRepository : IActorRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public ActorRepository(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public ICollection<Actor> Get()
    {
        return _applicationDbContext.Actors.ToList();
    }

    public async Task<Actor?> GetById(int id)
    {
        return await _applicationDbContext.Actors.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<bool> Create(Actor actor, CancellationToken cancellationToken)
    {
        await _applicationDbContext.Actors.AddAsync(actor);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> Update(Actor actor, CancellationToken cancellationToken)
    {
        var dbActor = await _applicationDbContext.Actors.FirstOrDefaultAsync(a => a.Id == actor.Id);
        if (dbActor == null)
            return false;

        dbActor.FirstName = actor.FirstName;
        dbActor.LastName = actor.LastName;
        dbActor.DateOfBirth = actor.DateOfBirth;
        dbActor.Biography = actor.Biography;
        dbActor.PhotoLink = actor.PhotoLink;

        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        var actor = await _applicationDbContext.Actors.FirstOrDefaultAsync(a => a.Id == id);
        if (actor == null)
            return false;

        _applicationDbContext.Actors.Remove(actor);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }
}
