using MediatR;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Genres.Queries.Get;

public record GetGenresQuery : IRequest<ICollection<GenreDto>>;