using Microsoft.AspNetCore.Http;

namespace MvDb.Application.Common.Interfaces;

public interface IPhotoEntityService<TEntity, TSearch> : IEntityService<TEntity, TSearch>
{
    Task<bool> Create(TEntity entity, IFormFile posterFile, CancellationToken cancellationToken);
    Task<bool> Update(TEntity entity, IFormFile posterFile, CancellationToken cancellationToken);
}
