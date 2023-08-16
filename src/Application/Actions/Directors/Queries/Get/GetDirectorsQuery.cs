using MediatR;
using MvDb.Application.Actions.Directors.DataTransferObjects;
namespace MvDb.Application.Actions.Directors.Queries.Get;

public record GetDirectorsQuery : IRequest<ICollection<DirectorDto>>;