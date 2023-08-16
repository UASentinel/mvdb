using MvDb.Domain.Entities;

namespace MvDb.Application.Common.Interfaces.Repositories;

public interface ISeasonRepository : IRepository<Season>
{
    Task<bool> UpdateSeasonsOrder(ICollection<Season> seasons, CancellationToken cancellationToken);
    Task<bool> RestoreMediaSeasonsOrder(int mediaId, CancellationToken cancellationToken);
}
