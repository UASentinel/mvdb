using MediatR;
using MvDb.Application.Actions.AgeRatings.DataTransferObjects;
using MvDb.Application.Actions.Episodes.DataTransferObjects;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Actions.Medias.DataTransferObjects.Objects;
using MvDb.Application.Actions.Seasons.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Medias.Queries.Get;

public class GetMediasCommandHandler : IRequestHandler<GetMediasQuery, ICollection<MediaDto>>
{
    private readonly IMediaService _mediaService;

    public GetMediasCommandHandler(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    public async Task<ICollection<MediaDto>> Handle(GetMediasQuery request, CancellationToken cancellationToken)
    {
        var medias = _mediaService.Get();

        if (medias == null)
            return new List<MediaDto>();

        var mediaDtos = new List<MediaDto>();
        foreach (var media in medias)
        {
            var mediaDto = new MediaDto()
            {
                Id = media.Id,
                Title = media.Title,
                Description = media.Description,
                TrailerLink = media.TrailerLink,
                PosterLink = media.PosterLink,
                MediaType = media.MediaType,
                AgeRating = new AgeRatingDto() {
                    Id = media.AgeRating.Id,
                    Name = media.AgeRating.Name,
                    MinAge = media.AgeRating.MinAge
                },
                Duration = media.Duration,
                ReleaseDate = media.ReleaseDate
            };

            mediaDto.Rating = _mediaService.CountRating(media);

            mediaDto.SetGenres(media.MediaGenres);
            mediaDto.SetSeasons(media.Seasons);

            mediaDto.ReleaseDate ??= DateTime.MinValue;

            mediaDtos.Add(mediaDto);
        }

        return mediaDtos;
    }
}
