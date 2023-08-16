namespace MvDb.Application.Common.Interfaces;

public interface IEntityService<TEntity, TSearch>
{
    ICollection<TEntity> Get();
    ICollection<TEntity> Search(TSearch searchPattern);
    Task<TEntity?> GetById(int id);
    Task<bool> Delete(int id, CancellationToken cancellationToken);
}
