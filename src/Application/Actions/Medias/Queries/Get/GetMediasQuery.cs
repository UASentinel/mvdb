using MediatR;
using MvDb.Application.Actions.Medias.DataTransferObjects.Objects;

namespace MvDb.Application.Actions.Medias.Queries.Get;

public record GetMediasQuery : IRequest<ICollection<MediaDto>>;