using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;

namespace MvDb.Application.Common.Interfaces.EntityServices;

public interface ISeasonService : IEntityService<Season>
{
    Task<bool> Create(Season season, IFormFile posterFile, CancellationToken cancellationToken);
    Task<bool> Update(Season season, IFormFile posterFile, CancellationToken cancellationToken);
}
