using MvDb.Domain.Entities;

namespace MvDb.Application.Common.Interfaces.EntityServices;

public interface IEpisodeService : IBaseEntityService<Episode, object>
{
    Task<bool> UpdateEpisodesOrder(ICollection<Episode> episodes, CancellationToken cancellationToken);
}
