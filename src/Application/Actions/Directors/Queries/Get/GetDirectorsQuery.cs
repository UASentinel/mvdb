using MediatR;
using MvDb.Application.Actions.Directors.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
namespace MvDb.Application.Actions.Directors.Queries.Get;

public record GetDirectorsQuery : IRequest<ICollection<DirectorDto>>;