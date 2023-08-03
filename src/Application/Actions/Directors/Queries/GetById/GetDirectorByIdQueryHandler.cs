using MediatR;
using MvDb.Application.Actions.Actors.DataTransferObjects;
using MvDb.Application.Actions.Directors.DataTransferObjects;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Directors.Queries.GetById;

public class GetDirectorByIdQueryHandler : IRequestHandler<GetDirectorByIdQuery, DirectorDto>
{
    private readonly IDirectorService _directorService;

    public GetDirectorByIdQueryHandler(IDirectorService directorService)
    {
        _directorService = directorService;
    }

    public async Task<DirectorDto> Handle(GetDirectorByIdQuery request, CancellationToken cancellationToken)
    {
        var director = await _directorService.GetById(request.Id);

        if (director == null)
            return null;

        var directorDto = new DirectorDto()
        {
            Id = director.Id,
            FirstName = director.FirstName,
            LastName = director.LastName,
            DateOfBirth = director.DateOfBirth,
            Biography = director.Biography,
            PhotoLink = director.PhotoLink
        };
        return directorDto;
    }
}
