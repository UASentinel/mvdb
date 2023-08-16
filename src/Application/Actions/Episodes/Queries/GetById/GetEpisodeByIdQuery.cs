using MediatR;
using MvDb.Application.Actions.Episodes.DataTransferObjects;

namespace MvDb.Application.Actions.Episodes.Queries.GetById;

public record GetEpisodeByIdQuery(int Id) : IRequest<EpisodeDto>;