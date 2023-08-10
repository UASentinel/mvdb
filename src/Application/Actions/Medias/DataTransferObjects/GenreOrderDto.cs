using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvDb.Application.Actions.Genres.DataTransferObjects;

namespace MvDb.Application.Actions.Medias.DataTransferObjects;
public class GenreOrderDto : GenreDto
{
    public byte Order { get; set; }
}
