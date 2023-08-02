using MediatR;
using MvDb.Application.Actions.AgeRatings.DataTransferObjects;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.AgeRatings.Queries.Get;

public record GetAgeRatingsQuery : IRequest<ICollection<AgeRatingDto>>;