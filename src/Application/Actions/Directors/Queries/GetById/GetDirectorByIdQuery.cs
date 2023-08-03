using MediatR;
using MvDb.Application.Actions.Actors.DataTransferObjects;
using MvDb.Application.Actions.Directors.DataTransferObjects;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Directors.Queries.GetById;

public record GetDirectorByIdQuery(int Id) : IRequest<DirectorDto>;