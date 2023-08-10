using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvDb.Application.Actions.Directors.DataTransferObjects;

namespace MvDb.Application.Actions.Medias.DataTransferObjects;
public class DirectorOrderDto : DirectorDto
{
    public byte Order { get; set; }
}
