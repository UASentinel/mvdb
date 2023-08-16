using MediatR;
using MvDb.Application.Actions.Directors.DataTransferObjects;
using MvDb.Application.Common.Interfaces.EntityServices;

namespace MvDb.Application.Actions.Directors.Queries.Get;

public class GetDirectorsCommandHandler : IRequestHandler<GetDirectorsQuery, ICollection<DirectorDto>>
{
    private readonly IDirectorService _directorService;

    public GetDirectorsCommandHandler(IDirectorService directorService)
    {
        _directorService = directorService;
    }

    public async Task<ICollection<DirectorDto>> Handle(GetDirectorsQuery request, CancellationToken cancellationToken)
    {
        var directors = _directorService.Get();

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
