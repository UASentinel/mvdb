using MediatR;
using MvDb.Application.Actions.AgeRatings.DataTransferObjects;

namespace MvDb.Application.Actions.AgeRatings.Queries.Get;

public record GetAgeRatingsQuery : IRequest<ICollection<AgeRatingDto>>;