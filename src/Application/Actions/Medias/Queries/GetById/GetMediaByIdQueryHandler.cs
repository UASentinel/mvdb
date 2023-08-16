using MediatR;
using MvDb.Application.Actions.AgeRatings.DataTransferObjects;
using MvDb.Application.Actions.Medias.DataTransferObjects.Objects;
using MvDb.Application.Common.Interfaces.EntityServices;

namespace MvDb.Application.Actions.Medias.Queries.GetById;

public class GetMediaByIdQueryHandler : IRequestHandler<GetMediaByIdQuery, MediaDto>
{
    private readonly IMediaService _mediaService;

    public GetMediaByIdQueryHandler(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    public async Task<MediaDto> Handle(GetMediaByIdQuery request, CancellationToken cancellationToken)
    {
        var media = await _mediaService.GetById(request.Id);

        if (media == null)
            return null;

        var mediaDto = new MediaDto()
        {
            Id = media.Id,
            Title = media.Title,
            Description = media.Description,
            TrailerLink = media.TrailerLink,
            PosterLink = media.PosterLink,
            MediaType = media.MediaType,
            AgeRating = new AgeRatingDto()
            {
                Id = media.AgeRating.Id,
                Name = media.AgeRating.Name,
                MinAge = media.AgeRating.MinAge
            },
            Duration = media.Duration,
            ReleaseDate = media.ReleaseDate
        };

        mediaDto.Rating = _mediaService.CountRating(media);

        mediaDto.SetGenres(media.MediaGenres);
        mediaDto.SetActors(media.MediaActors);
        mediaDto.SetDirectors(media.MediaDirectors);
        mediaDto.SetSeasons(media.Seasons);

        mediaDto.ReleaseDate ??= DateTime.MinValue;

        return mediaDto;
    }
}
