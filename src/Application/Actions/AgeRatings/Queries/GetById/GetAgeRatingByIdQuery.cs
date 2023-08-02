using MediatR;
using MvDb.Application.Actions.AgeRatings.DataTransferObjects;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.AgeRatings.Queries.GetById;

public record GetAgeRatingByIdQuery(int Id) : IRequest<AgeRatingDto>;