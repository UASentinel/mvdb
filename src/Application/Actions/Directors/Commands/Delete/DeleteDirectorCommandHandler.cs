using MediatR;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Directors.Commands.Delete;

public class DeleteDirectorCommandHandler : IRequestHandler<DeleteDirectorCommand>
{
    private readonly IDirectorService _directorService;

    public DeleteDirectorCommandHandler(IDirectorService directorService)
    {
        _directorService = directorService;
    }

    public async Task Handle(DeleteDirectorCommand request, CancellationToken cancellationToken)
    {
        await _directorService.Delete(request.Id, cancellationToken);
    }
}
