using MediatR;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Actions.Medias.DataTransferObjects.Relationships;
using MvDb.Application.Actions.Seasons.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
using MvDb.Domain.Entities;
using MvDb.Domain.Enums;

namespace MvDb.Application.Actions.Seasons.Commands.UpdateEpisodesOrder;

public record UpdateEpisodesOrderCommand : IRequest
{
    public int SeasonId { get; set; }
    public ICollection<SeasonEpisodeDto> SeasonEpisodeDtos { get; set; } = new List<SeasonEpisodeDto>();
}