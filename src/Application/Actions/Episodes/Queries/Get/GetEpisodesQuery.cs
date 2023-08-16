using MediatR;
using MvDb.Application.Actions.Episodes.DataTransferObjects;

namespace MvDb.Application.Actions.Episodes.Queries.Get;

public record GetEpisodesQuery : IRequest<ICollection<EpisodeDto>>;