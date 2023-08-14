using MediatR;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Actions.Medias.DataTransferObjects.Relationships;
using MvDb.Application.Common.Interfaces;
using MvDb.Domain.Entities;
using MvDb.Domain.Enums;

namespace MvDb.Application.Actions.Medias.Commands.UpdateActors;

public record UpdateActorsCommand : IRequest
{
    public int MediaId { get; set; }
    public ICollection<MediaActorDto> MediaActorDtos { get; set; } = new List<MediaActorDto>();
}