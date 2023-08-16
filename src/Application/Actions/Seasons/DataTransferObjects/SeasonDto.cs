using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvDb.Application.Actions.Episodes.DataTransferObjects;
using MvDb.Application.Actions.Medias.DataTransferObjects.Objects;
using MvDb.Domain.Entities;
using MvDb.Domain.Enums;

namespace MvDb.Application.Actions.Seasons.DataTransferObjects;
public class SeasonDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Order { get; set; }
    public string? PosterLink { get; set; }
    public string? TrailerLink { get; set; }
    public int Duration { get; set; }
    public DateTime ReleaseDate { get; set; }
    public ICollection<EpisodeDto> Episodes { get; set;}
    public int MediaId { get; set; }
    public void SetEpisodes(ICollection<Episode> episodes)
    {
        Episodes ??= new List<EpisodeDto>();

        var releaseDate = DateTime.MaxValue;

        foreach (var episode in episodes)
        {
            var episodeDto = new EpisodeDto()
            {
                Id = episode.Id,
                Title = episode.Title,
                Order = episode.Order,
                Description = episode.Description,
                Duration = episode.Duration,
                ReleaseDate = episode.ReleaseDate
            };

            if (episode.ReleaseDate < releaseDate)
                releaseDate = episode.ReleaseDate;

            Duration += episode.Duration;

            if (releaseDate != DateTime.MaxValue)
                ReleaseDate = releaseDate;

            Episodes.Add(episodeDto);
        }
    }
}
