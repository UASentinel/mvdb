using MvDb.Domain.Entities;

namespace MvDb.Application.Common.Interfaces.Repositories;

public interface IEpisodeRepository : IRepository<Episode>
{
    Task<bool> UpdateEpisodesOrder(ICollection<Episode> episodes, CancellationToken cancellationToken);
    Task<bool> RestoreSeasonEpisodesOrder(int seasonId, CancellationToken cancellationToken);
}
