using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Application.Common.Interfaces.Repositories;
using MvDb.Domain.Entities;

namespace MvDb.Application.Services;

public class AgeRatingService : IAgeRatingService
{
    private readonly IAgeRatingRepository _ageRatingRepository;
    public AgeRatingService(IAgeRatingRepository ageRatingRepository)
    {
        _ageRatingRepository = ageRatingRepository;
    }
    public ICollection<AgeRating> Get()
    {
        return _ageRatingRepository.Get();
    }

    public async Task<AgeRating?> GetById(int id)
    {
        return await _ageRatingRepository.GetById(id);
    }

    public async Task<bool> Create(AgeRating ageRating, CancellationToken cancellationToken)
    {
        return await _ageRatingRepository.Create(ageRating, cancellationToken);
    }

    public async Task<bool> Update(AgeRating ageRating, CancellationToken cancellationToken)
    {
        return await _ageRatingRepository.Update(ageRating, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        return await _ageRatingRepository.Delete(id, cancellationToken);
    }

    public ICollection<AgeRating> Search(object searchPattern)
    {
        throw new NotImplementedException();
    }
}
