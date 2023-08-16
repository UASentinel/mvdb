using MediatR;
using MvDb.Application.Actions.AgeRatings.DataTransferObjects;

namespace MvDb.Application.Actions.AgeRatings.Queries.GetById;

public record GetAgeRatingByIdQuery(int Id) : IRequest<AgeRatingDto>;