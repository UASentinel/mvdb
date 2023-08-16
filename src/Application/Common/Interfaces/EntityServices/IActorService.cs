using MvDb.Application.Actions.Actors.Queries.Search;
using MvDb.Domain.Entities;

namespace MvDb.Application.Common.Interfaces.EntityServices;

public interface IActorService : IPhotoEntityService<Actor, SearchActorsQuery>
{
    
}
