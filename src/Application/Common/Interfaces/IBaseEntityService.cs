namespace MvDb.Application.Common.Interfaces;

public interface IBaseEntityService<TEntity> : IEntityService<TEntity> where TEntity : class
{
    Task<bool> Create(TEntity entity, CancellationToken cancellationToken);
    Task<bool> Update(TEntity entity, CancellationToken cancellationToken);
}
