using MediatR;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Genres.Commands.Delete;

public record DeleteGenreCommand(int Id) : IRequest;