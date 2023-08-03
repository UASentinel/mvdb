using MediatR;
using MvDb.Application.Actions.Episodes.DataTransferObjects;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Actions.Seasons.DataTransferObjects;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Seasons.Queries.Get;

public record GetSeasonsQuery : IRequest<ICollection<SeasonDto>>;