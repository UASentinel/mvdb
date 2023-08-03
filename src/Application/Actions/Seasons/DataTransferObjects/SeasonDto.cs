using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Seasons.DataTransferObjects;
public class SeasonDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Order { get; set; }
    public string? PosterLink { get; set; }
    public string? TrailerLink { get; set; }
}
