using MediatR;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.AgeRatings.Commands.Update;

public record UpdateAgeRatingCommand : IRequest
{
    public int Id { get; init; }
    public string Name { get; init; }
    public byte MinAge { get; init; }
}