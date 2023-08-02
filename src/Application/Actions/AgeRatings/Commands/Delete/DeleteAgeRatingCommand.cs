using MediatR;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.AgeRatings.Commands.Delete;

public record DeleteAgeRatingCommand(int Id) : IRequest;