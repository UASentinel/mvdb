using MediatR;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.AgeRatings.Commands.Update;

public class UpdateAgeRatingCommandHandler : IRequestHandler<UpdateAgeRatingCommand>
{
    private readonly IAgeRatingService _ageRatingService;

    public UpdateAgeRatingCommandHandler(IAgeRatingService ageRatingService)
    {
        _ageRatingService = ageRatingService;
    }

    public async Task Handle(UpdateAgeRatingCommand request, CancellationToken cancellationToken)
    {
        var ageRating = new AgeRating()
        {
            Id = request.AgeRatingId,
            Name = request.Name,
            MinAge = request.MinAge
        };

        await _ageRatingService.Update(ageRating, cancellationToken);
    }
}
