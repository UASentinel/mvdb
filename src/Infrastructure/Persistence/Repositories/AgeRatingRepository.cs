using Microsoft.EntityFrameworkCore;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.Repositories;
using MvDb.Domain.Entities;

namespace MvDb.Infrastructure.Repositories;

public class AgeRatingRepository : IAgeRatingRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public AgeRatingRepository(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public ICollection<AgeRating> Get()
    {
        return _applicationDbContext.AgeRatings.ToList();
    }

    public async Task<AgeRating?> GetById(int id)
    {
        return await _applicationDbContext.AgeRatings.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<bool> Create(AgeRating ageRating, CancellationToken cancellationToken)
    {
        await _applicationDbContext.AgeRatings.AddAsync(ageRating);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> Update(AgeRating ageRating, CancellationToken cancellationToken)
    {
        var dbAgeRating = await _applicationDbContext.AgeRatings.FirstOrDefaultAsync(a => a.Id == ageRating.Id);
        if (dbAgeRating == null)
            return false;

        dbAgeRating.Name = ageRating.Name;
        dbAgeRating.MinAge = ageRating.MinAge;

        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        var ageRating = await _applicationDbContext.AgeRatings.FirstOrDefaultAsync(a => a.Id == id);
        if (ageRating == null)
            return false;

        _applicationDbContext.AgeRatings.Remove(ageRating);
        var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0 ? true : false;
    }
}
