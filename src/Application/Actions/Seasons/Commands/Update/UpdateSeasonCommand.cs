using MediatR;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Seasons.Commands.Update;

public record UpdateSeasonCommand : IRequest
{
    public int SeasonId { get; init; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Order { get; set; }
    public IFormFile? PosterFile { get; set; }
    public bool DeletePoster { get; set; }
    public string? TrailerLink { get; set; }
    public int MediaId { get; set; }
}