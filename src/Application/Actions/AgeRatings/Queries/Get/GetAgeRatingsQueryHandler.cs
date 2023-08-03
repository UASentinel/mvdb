using MediatR;
using MvDb.Application.Actions.AgeRatings.DataTransferObjects;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Actions.Genres.Queries.Get;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.AgeRatings.Queries.Get;

public class GetAgeRatingsCommandHandler : IRequestHandler<GetAgeRatingsQuery, ICollection<AgeRatingDto>>
{
    private readonly IAgeRatingService _ageRatingService;

    public GetAgeRatingsCommandHandler(IAgeRatingService ageRatingService)
    {
        _ageRatingService = ageRatingService;
    }

    public async Task<ICollection<AgeRatingDto>> Handle(GetAgeRatingsQuery request, CancellationToken cancellationToken)
    {
        var ageRatings = _ageRatingService.Get();

        if (ageRatings == null)
            return new List<AgeRatingDto>();

        var ageRatingDtos = new List<AgeRatingDto>();
        foreach (var ageRating in ageRatings)
        {
            var ageRatingDto = new AgeRatingDto()
            {
                Id = ageRating.Id,
                Name = ageRating.Name,
                MinAge = ageRating.MinAge
            };

            ageRatingDtos.Add(ageRatingDto);
        }

        return ageRatingDtos;
    }
}
