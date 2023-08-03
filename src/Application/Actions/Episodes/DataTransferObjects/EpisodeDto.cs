using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvDb.Application.Actions.Episodes.DataTransferObjects;
public class EpisodeDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Duration { get; set; }
    public int Order { get; set; }
    public DateTime ReleaseDate { get; set; }
}
