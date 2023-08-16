using MvDb.Application.Actions.Directors.Queries.Search;
using MvDb.Domain.Entities;

namespace MvDb.Application.Common.Interfaces.EntityServices;

public interface IDirectorService : IPhotoEntityService<Director, SearchDirectorsQuery>
{
    
}
