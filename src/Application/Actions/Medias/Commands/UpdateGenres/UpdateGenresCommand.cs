using MediatR;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Actions.Medias.DataTransferObjects.Relationships;
using MvDb.Application.Common.Interfaces;
using MvDb.Domain.Entities;
using MvDb.Domain.Enums;

namespace MvDb.Application.Actions.Medias.Commands.AddGenres;

public record UpdateGenresCommand : IRequest
{
    public int MediaId { get; set; }
    public ICollection<MediaGenreDto> MediaGenreDtos { get; set; } = new List<MediaGenreDto>();
}