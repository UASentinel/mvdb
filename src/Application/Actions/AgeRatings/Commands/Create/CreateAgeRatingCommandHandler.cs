using MediatR;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.AgeRatings.Commands.Create;

public class CreateAgeRatingCommandHandler : IRequestHandler<CreateAgeRatingCommand, int>
{
    private readonly IAgeRatingService _ageRatingService;

    public CreateAgeRatingCommandHandler(IAgeRatingService ageRatingService)
    {
        _ageRatingService = ageRatingService;
    }

    public async Task<int> Handle(CreateAgeRatingCommand request, CancellationToken cancellationToken)
    {
        var ageRating = new AgeRating()
        {
            Name = request.Name,
            MinAge = request.MinAge
        };

        await _ageRatingService.Create(ageRating, cancellationToken);

        return ageRating.Id;
    }
}
