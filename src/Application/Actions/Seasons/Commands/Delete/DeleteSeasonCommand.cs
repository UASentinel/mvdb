using MediatR;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Seasons.Commands.Delete;

public record DeleteSeasonCommand(int Id) : IRequest;