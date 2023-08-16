using MediatR;
using MvDb.Application.Actions.Directors.DataTransferObjects;

namespace MvDb.Application.Actions.Directors.Queries.GetById;

public record GetDirectorByIdQuery(int Id) : IRequest<DirectorDto>;