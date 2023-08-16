using MediatR;
using MvDb.Application.Actions.AgeRatings.DataTransferObjects;
using MvDb.Application.Common.Interfaces.EntityServices;

namespace MvDb.Application.Actions.AgeRatings.Queries.GetById;

public class GetAgeRatingByIdQueryHandler : IRequestHandler<GetAgeRatingByIdQuery, AgeRatingDto>
{
    private readonly IAgeRatingService _ageRatingService;

    public GetAgeRatingByIdQueryHandler(IAgeRatingService ageRatingService)
    {
        _ageRatingService = ageRatingService;
    }

    public async Task<AgeRatingDto> Handle(GetAgeRatingByIdQuery request, CancellationToken cancellationToken)
    {
        var ageRating = await _ageRatingService.GetById(request.Id);

        if (ageRating == null)
            return null;

        var ageRatingDto = new AgeRatingDto()
        {
            Id = ageRating.Id,
            Name = ageRating.Name,
            MinAge = ageRating.MinAge
        };
        return ageRatingDto;
    }
}
