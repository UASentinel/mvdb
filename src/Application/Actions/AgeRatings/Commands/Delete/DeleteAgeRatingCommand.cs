using MediatR;

namespace MvDb.Application.Actions.AgeRatings.Commands.Delete;

public record DeleteAgeRatingCommand(int Id) : IRequest;