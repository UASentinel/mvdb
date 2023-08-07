using MediatR;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Actions.Medias.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
using MvDb.Domain.Entities;
using MvDb.Domain.Enums;

namespace MvDb.Application.Actions.Medias.Commands.Create;

public record CreateMediaCommand : IRequest<int>
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public MediaType MediaType { get; set; }
    public IFormFile? PosterFile { get; set; }
    public string? TrailerLink { get; set; }
    public int AgeRatingId { get; set; }
    public int Duration { get; set; }
    public DateTime? ReleaseDate { get; set; }
}