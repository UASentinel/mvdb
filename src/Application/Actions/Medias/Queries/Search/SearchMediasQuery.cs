using MediatR;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Actions.Episodes.DataTransferObjects;
using MvDb.Application.Actions.Medias.DataTransferObjects;
using MvDb.Application.Actions.Seasons.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
using MvDb.Domain.Enums;

namespace MvDb.Application.Actions.Medias.Queries.Search;

public record SearchMediasQuery : IRequest<ICollection<MediaDto>>
{
    public string Title { get; set; }
    public MediaType MediaType { get; set; }
}