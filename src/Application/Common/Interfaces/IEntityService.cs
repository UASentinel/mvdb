using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Models;

namespace MvDb.Application.Common.Interfaces;

public interface IEntityService<TEntity> where TEntity : class
{
    ICollection<TEntity> Get();
    Task<TEntity?> GetById(int id);
    Task<bool> Delete(int id, CancellationToken cancellationToken);
}
