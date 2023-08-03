using MediatR;
using MvDb.Application.Actions.Episodes.DataTransferObjects;
using MvDb.Application.Actions.Seasons.DataTransferObjects;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Seasons.Queries.GetById;

public record GetSeasonByIdQuery(int Id) : IRequest<SeasonDto>;