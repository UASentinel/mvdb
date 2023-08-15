using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MvDb.Application.Actions.AgeRatings.DataTransferObjects;
using MvDb.Domain.Entities;
using MvDb.Domain.Enums;

namespace MvDb.Application.Actions.Medias.DataTransferObjects.Objects;
public class MediaDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public MediaType MediaType { get; set; }
    public string? PosterLink { get; set; }
    public string? TrailerLink { get; set; }
    public AgeRatingDto AgeRating { get; set; }
    public int Duration { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public byte Rating { get; set; }
    public ICollection<GenreOrderDto> Genres { get; set; }
    public ICollection<ActorOrderDto> Actors { get; set; }
    public ICollection<DirectorOrderDto> Directors { get; set; }
    public ICollection<SeasonBriefDto> Seasons { get; set; }
    public void SetGenres(ICollection<MediaGenre> mediaGenres)
    {
        Genres ??= new List<GenreOrderDto>();

        foreach (var mediaGenre in mediaGenres)
        {
            if (mediaGenre.Genre != null)
            {
                Genres.Add(new GenreOrderDto()
                {
                    Id = mediaGenre.Genre.Id,
                    Name = mediaGenre.Genre.Name,
                    Order = mediaGenre.Order
                });
            }
        }
    }

    public void SetActors(ICollection<MediaActor> mediaActors)
    {
        Actors ??= new List<ActorOrderDto>();

        foreach (var mediaActor in mediaActors)
        {
            if (mediaActor.Actor != null)
            {
                Actors.Add(new ActorOrderDto()
                {
                    Id = mediaActor.Actor.Id,
                    FirstName = mediaActor.Actor.FirstName,
                    LastName = mediaActor.Actor.LastName,
                    DateOfBirth = mediaActor.Actor.DateOfBirth,
                    Biography = mediaActor.Actor.Biography,
                    PhotoLink = mediaActor.Actor.PhotoLink,
                    Order = mediaActor.Order
                });
            }
        }
    }

    public void SetDirectors(ICollection<MediaDirector> mediaDirectors)
    {
        Directors ??= new List<DirectorOrderDto>();

        foreach (var mediaDirector in mediaDirectors)
        {
            if (mediaDirector.Director != null)
            {
                Directors.Add(new DirectorOrderDto()
                {
                    Id = mediaDirector.Director.Id,
                    FirstName = mediaDirector.Director.FirstName,
                    LastName = mediaDirector.Director.LastName,
                    DateOfBirth = mediaDirector.Director.DateOfBirth,
                    Biography = mediaDirector.Director.Biography,
                    PhotoLink = mediaDirector.Director.PhotoLink,
                    Order = mediaDirector.Order
                });
            }
        }
    }

    public void SetSeasons(ICollection<Season> seasons)
    {
        Seasons ??= new List<SeasonBriefDto>();

        if (MediaType == MediaType.Movie)
            return;

        foreach (var season in seasons)
        {
            var seasonDto = new SeasonBriefDto()
            {
                Id =season.Id,
                Title = season.Title,
                Order = season.Order,
                PosterLink = season.PosterLink,
                TrailerLink = season.TrailerLink
            };

            var releaseDate = DateTime.MaxValue;

            if (season.Episodes != null)
            {
                foreach (var episode in season.Episodes)
                {
                    if (episode.ReleaseDate < releaseDate)
                        releaseDate = episode.ReleaseDate;

                    seasonDto.Duration += episode.Duration;
                    seasonDto.EpisodeCount++;
                }
            }

            if (releaseDate != DateTime.MaxValue)
                ReleaseDate = releaseDate;

            Duration += seasonDto.Duration;
            Seasons.Add(seasonDto);
        }
    }
}
