using MediatR;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Actions.Medias.DataTransferObjects.Relationships;
using MvDb.Application.Common.Interfaces;
using MvDb.Domain.Entities;
using MvDb.Domain.Enums;

namespace MvDb.Application.Actions.Medias.Commands.UpdateSeasonsOrder;

public record UpdateSeasonsOrderCommand : IRequest
{
    public int MediaId { get; set; }
    public ICollection<MediaSeasonDto> MediaSeasonDtos { get; set; } = new List<MediaSeasonDto>();
}