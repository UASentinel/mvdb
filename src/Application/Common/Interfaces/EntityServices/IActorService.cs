using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;

namespace MvDb.Application.Common.Interfaces.EntityServices;

public interface IActorService : IEntityService<Actor>
{
    Task<bool> Create(Actor actor, IFormFile photoFile, CancellationToken cancellationToken);
    Task<bool> Update(Actor actor, IFormFile photoFile, CancellationToken cancellationToken);
}
