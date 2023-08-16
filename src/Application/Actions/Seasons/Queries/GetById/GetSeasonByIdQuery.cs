using MediatR;
using MvDb.Application.Actions.Seasons.DataTransferObjects;

namespace MvDb.Application.Actions.Seasons.Queries.GetById;

public record GetSeasonByIdQuery(int Id) : IRequest<SeasonDto>;