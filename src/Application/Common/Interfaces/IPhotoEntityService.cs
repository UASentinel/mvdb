using Microsoft.AspNetCore.Http;

namespace MvDb.Application.Common.Interfaces;

public interface IPhotoEntityService<TEntity> : IEntityService<TEntity> where TEntity : class
{
    Task<bool> Create(TEntity entity, IFormFile posterFile, CancellationToken cancellationToken);
    Task<bool> Update(TEntity entity, IFormFile posterFile, CancellationToken cancellationToken);
}
