namespace MvDb.Application.Common.Interfaces;

public interface IBaseEntityService<TEntity, TSearch> : IEntityService<TEntity, TSearch>
{
    Task<bool> Create(TEntity entity, CancellationToken cancellationToken);
    Task<bool> Update(TEntity entity, CancellationToken cancellationToken);
}
