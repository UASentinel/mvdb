using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Models;

namespace MvDb.Application.Common.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    ICollection<TEntity> Get();
    Task<TEntity?> GetById(int id);
    Task<bool> Create(TEntity entity, CancellationToken cancellationToken);
    Task<bool> Update(TEntity entity, CancellationToken cancellationToken);
    Task<bool> Delete(int id, CancellationToken cancellationToken);
}
