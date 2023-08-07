using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvDb.Application.Actions.AgeRatings.DataTransferObjects;
using MvDb.Domain.Entities;
using MvDb.Domain.Enums;

namespace MvDb.Application.Actions.Medias.DataTransferObjects;
public class MediaDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public MediaType MediaType { get; set; }
    public string? PosterLink { get; set; }
    public string? TrailerLink { get; set; }
    public AgeRatingDto AgeRating { get; set; }
    public int Duration { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public byte Rating { get; set; }
}
