using MediatR;
using MvDb.Application.Actions.AgeRatings.DataTransferObjects;
using MvDb.Application.Actions.Episodes.DataTransferObjects;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Actions.Medias.DataTransferObjects;
using MvDb.Application.Actions.Seasons.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

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

            if (mediaDto.ReleaseDate == null)
                mediaDto.ReleaseDate = DateTime.MinValue;

            mediaDtos.Add(mediaDto);
        }

        return mediaDtos;
    }
}
