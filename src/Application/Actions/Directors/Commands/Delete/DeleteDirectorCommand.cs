using MediatR;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Directors.Commands.Delete;

public record DeleteDirectorCommand(int Id) : IRequest;