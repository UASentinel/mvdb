using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;

namespace MvDb.Application.Common.Interfaces.EntityServices;

public interface IAgeRatingService : IEntityService<AgeRating>
{
    Task<bool> Create(AgeRating ageRating, CancellationToken cancellationToken);
    Task<bool> Update(AgeRating ageRating, CancellationToken cancellationToken);
}
