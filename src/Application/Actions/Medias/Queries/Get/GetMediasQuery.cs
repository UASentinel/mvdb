using MediatR;
using MvDb.Application.Actions.Episodes.DataTransferObjects;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Actions.Medias.DataTransferObjects.Objects;
using MvDb.Application.Actions.Seasons.DataTransferObjects;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Medias.Queries.Get;

public record GetMediasQuery : IRequest<ICollection<MediaDto>>;