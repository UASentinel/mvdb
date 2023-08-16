using MediatR;
using MvDb.Application.Actions.Genres.DataTransferObjects;

namespace MvDb.Application.Actions.Genres.Queries.Get;

public record GetGenresQuery : IRequest<ICollection<GenreDto>>;