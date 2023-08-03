using MediatR;
using MvDb.Application.Actions.Actors.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Directors.Commands.Create;

public class CreateDirectorCommandHandler : IRequestHandler<CreateDirectorCommand, int>
{
    private readonly IDirectorService _directorService;

    public CreateDirectorCommandHandler(IDirectorService directorService)
    {
        _directorService = directorService;
    }

    public async Task<int> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
    {
        var director = new Director()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Biography = request.Biography,
            PhotoLink = null
        };

        await _directorService.Create(director, request.PhotoFile, cancellationToken);

        return director.Id;
    }
}
