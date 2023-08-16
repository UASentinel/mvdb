using MediatR;
using MvDb.Application.Common.Interfaces.EntityServices;

namespace MvDb.Application.Actions.AgeRatings.Commands.Delete;

public class DeleteAgeRatingCommandHandler : IRequestHandler<DeleteAgeRatingCommand>
{
    private readonly IAgeRatingService _ageRatingService;

    public DeleteAgeRatingCommandHandler(IAgeRatingService ageRatingService)
    {
        _ageRatingService = ageRatingService;
    }

    public async Task Handle(DeleteAgeRatingCommand request, CancellationToken cancellationToken)
    {
        await _ageRatingService.Delete(request.Id, cancellationToken);
    }
}
