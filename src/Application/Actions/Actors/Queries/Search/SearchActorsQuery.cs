using MediatR;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Actions.Actors.DataTransferObjects;
using MvDb.Application.Actions.Directors.DataTransferObjects;
using MvDb.Application.Actions.Episodes.DataTransferObjects;
using MvDb.Application.Actions.Medias.DataTransferObjects;
using MvDb.Application.Actions.Seasons.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
using MvDb.Domain.Enums;

namespace MvDb.Application.Actions.Actors.Queries.Search;

public record SearchActorsQuery : IRequest<ICollection<ActorDto>>
{
    public string Name { get; set; }
}