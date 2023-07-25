//using MvDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MvDb.Application.Common.Interfaces;

public interface IApplicationDbContext
{

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
