using MediatR;
using MvDb.Application.Actions.Medias.DataTransferObjects.Objects;

namespace MvDb.Application.Actions.Medias.Queries.GetById;

public record GetMediaByIdQuery(int Id) : IRequest<MediaDto>;