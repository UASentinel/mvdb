using MediatR;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.AgeRatings.Commands.Create;

public record CreateAgeRatingCommand : IRequest<int>
{
    public string Name { get; init; }
    public byte MinAge { get; init; }
}