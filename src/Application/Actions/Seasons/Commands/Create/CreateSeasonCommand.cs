using MediatR;
using Microsoft.AspNetCore.Http;

namespace MvDb.Application.Actions.Seasons.Commands.Create;

public record CreateSeasonCommand : IRequest<int>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Order { get; set; }
    public IFormFile? PosterFile { get; set; }
    public string? TrailerLink { get; set; }
    public int MediaId { get; set; }
}