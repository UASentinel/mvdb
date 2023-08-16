using MediatR;
using MvDb.Application.Actions.AgeRatings.DataTransferObjects;
using MvDb.Application.Actions.Medias.DataTransferObjects.Objects;
using MvDb.Application.Common.Interfaces.EntityServices;

namespace MvDb.Application.Actions.Medias.Queries.Search;

public class SearchMediasQueryHandler : IRequestHandler<SearchMediasQuery, ICollection<MediaDto>>
{
    private readonly IMediaService _mediaService;

    public SearchMediasQueryHandler(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    public async Task<ICollection<MediaDto>> Handle(SearchMediasQuery request, CancellationToken cancellationToken)
    {
        var medias = _mediaService.Search(request);

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
            mediaDto.SetSeasons(media.Seasons);

            mediaDto.ReleaseDate ??= DateTime.MinValue;

            mediaDtos.Add(mediaDto);
        }

        return mediaDtos;
    }
}
