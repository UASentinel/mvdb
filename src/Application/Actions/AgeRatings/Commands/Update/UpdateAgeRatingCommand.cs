using MediatR;

namespace MvDb.Application.Actions.AgeRatings.Commands.Update;

public record UpdateAgeRatingCommand : IRequest
{
    public int AgeRatingId { get; init; }
    public string Name { get; init; }
    public byte MinAge { get; init; }
}