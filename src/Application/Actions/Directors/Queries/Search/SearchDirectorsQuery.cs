using MediatR;
using MvDb.Application.Actions.Directors.DataTransferObjects;

namespace MvDb.Application.Actions.Directors.Queries.Search;

public record SearchDirectorsQuery : IRequest<ICollection<DirectorDto>>
{
    public string Name { get; set; }
}