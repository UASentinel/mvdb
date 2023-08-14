using Microsoft.AspNetCore.Http;
using MvDb.Application.Actions.Actors.Queries.Search;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;

namespace MvDb.Application.Common.Interfaces.EntityServices;

public interface IActorService : IPhotoEntityService<Actor, SearchActorsQuery>
{
    
}
