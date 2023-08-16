using MediatR;
using MvDb.Application.Actions.Medias.DataTransferObjects.Objects;
using MvDb.Domain.Enums;

namespace MvDb.Application.Actions.Medias.Queries.Search;

public record SearchMediasQuery : IRequest<ICollection<MediaDto>>
{
    public string Title { get; set; }
    public MediaType MediaType { get; set; }
}