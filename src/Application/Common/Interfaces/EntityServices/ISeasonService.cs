using MvDb.Domain.Entities;

namespace MvDb.Application.Common.Interfaces.EntityServices;

public interface ISeasonService : IPhotoEntityService<Season, object>
{
    Task<bool> UpdateSeasonsOrder(ICollection<Season> seasons, CancellationToken cancellationToken);
}
