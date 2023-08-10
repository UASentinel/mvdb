using MediatR;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Actions.Medias.DataTransferObjects;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Genres.Queries.GetById;

public record GetGenreByIdQuery(int Id) : IRequest<GenreDto>;