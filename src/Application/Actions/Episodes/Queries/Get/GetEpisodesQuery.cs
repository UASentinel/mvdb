using MediatR;
using MvDb.Application.Actions.Episodes.DataTransferObjects;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Episodes.Queries.Get;

public record GetEpisodesQuery : IRequest<ICollection<EpisodeDto>>;