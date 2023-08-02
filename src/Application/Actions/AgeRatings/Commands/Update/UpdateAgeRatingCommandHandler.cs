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
        var ageRating = new AgeRating(id: request.Id, name: request.Name, minAge: request.MinAge);

        await _ageRatingService.Update(ageRating, cancellationToken);
    }
}
