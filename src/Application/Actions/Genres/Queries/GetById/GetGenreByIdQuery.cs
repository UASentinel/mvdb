using MediatR;
using MvDb.Application.Actions.Genres.DataTransferObjects;

namespace MvDb.Application.Actions.Genres.Queries.GetById;

public record GetGenreByIdQuery(int Id) : IRequest<GenreDto>;