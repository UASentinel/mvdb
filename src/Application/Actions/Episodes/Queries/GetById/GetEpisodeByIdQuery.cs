using MediatR;
using MvDb.Application.Actions.Episodes.DataTransferObjects;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Episodes.Queries.GetById;

public record GetEpisodeByIdQuery(int Id) : IRequest<EpisodeDto>;