using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;

namespace MvDb.Application.Common.Interfaces.EntityServices;

public interface IDirectorService : IEntityService<Director>
{
    Task<bool> Create(Director director, IFormFile photoFile, CancellationToken cancellationToken);
    Task<bool> Update(Director director, IFormFile photoFile, CancellationToken cancellationToken);
}
