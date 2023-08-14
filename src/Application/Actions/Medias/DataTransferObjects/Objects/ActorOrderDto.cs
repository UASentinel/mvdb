using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvDb.Application.Actions.Actors.DataTransferObjects;

namespace MvDb.Application.Actions.Medias.DataTransferObjects.Objects;
public class ActorOrderDto : ActorDto
{
    public byte Order { get; set; }
}
