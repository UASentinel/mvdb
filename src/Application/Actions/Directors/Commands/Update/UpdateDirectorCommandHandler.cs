using MediatR;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Directors.Commands.Update;

public class UpdateDirectorCommandHandler : IRequestHandler<UpdateDirectorCommand>
{
    private readonly IDirectorService _directorService;

    public UpdateDirectorCommandHandler(IDirectorService directorService)
    {
        _directorService = directorService;
    }

    public async Task Handle(UpdateDirectorCommand request, CancellationToken cancellationToken)
    {
        var director = new Director()
        {
            Id = request.DirectorId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Biography = request.Biography,
            PhotoLink = null
        };

        await _directorService.Update(director, request.PhotoFile, request.DeletePhoto, cancellationToken);
    }
}
