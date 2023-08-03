using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;

namespace MvDb.Application.Common.Interfaces.EntityServices;

public interface IGenreService : IEntityService<Genre>
{
    Task<bool> Create(Genre genre, CancellationToken cancellationToken);
    Task<bool> Update(Genre genre, CancellationToken cancellationToken);
}
