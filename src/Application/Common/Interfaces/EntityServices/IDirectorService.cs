using Microsoft.AspNetCore.Http;
using MvDb.Application.Actions.Actors.Queries.Search;
using MvDb.Application.Actions.Directors.Queries.Search;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;

namespace MvDb.Application.Common.Interfaces.EntityServices;

public interface IDirectorService : IPhotoEntityService<Director, SearchDirectorsQuery>
{
    
}
