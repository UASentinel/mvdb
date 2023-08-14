using MediatR;
using MvDb.Application.Actions.AgeRatings.DataTransferObjects;
using MvDb.Application.Actions.Directors.DataTransferObjects;
using MvDb.Application.Actions.Episodes.DataTransferObjects;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Actions.Medias.DataTransferObjects;
using MvDb.Application.Actions.Seasons.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Directors.Queries.Search;

public class SearchDirectorsQueryHandler : IRequestHandler<SearchDirectorsQuery, ICollection<DirectorDto>>
{
    private readonly IDirectorService _directorService;

    public SearchDirectorsQueryHandler(IDirectorService directorService)
    {
        _directorService = directorService;
    }

    public async Task<ICollection<DirectorDto>> Handle(SearchDirectorsQuery request, CancellationToken cancellationToken)
    {
        var directors = _directorService.Search(request);

        if (directors == null)
            return new List<DirectorDto>();

        var directorDtos = new List<DirectorDto>();
        foreach (var director in directors)
        {
            var directorDto = new DirectorDto()
            {
                Id = director.Id,
                FirstName = director.FirstName,
                LastName = director.LastName,
                DateOfBirth = director.DateOfBirth,
                Biography = director.Biography,
                PhotoLink = director.PhotoLink
            };

            directorDtos.Add(directorDto);
        }

        return directorDtos;
    }
}
