using MediatR;
using MvDb.Application.Actions.Seasons.DataTransferObjects;

namespace MvDb.Application.Actions.Seasons.Queries.Get;

public record GetSeasonsQuery : IRequest<ICollection<SeasonDto>>;