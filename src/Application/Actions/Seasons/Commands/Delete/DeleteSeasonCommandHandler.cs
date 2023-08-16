using MediatR;
using MvDb.Application.Common.Interfaces.EntityServices;

namespace MvDb.Application.Actions.Seasons.Commands.Delete;

public class DeleteSeasonCommandHandler : IRequestHandler<DeleteSeasonCommand>
{
    private readonly ISeasonService _seasonService;

    public DeleteSeasonCommandHandler(ISeasonService seasonService)
    {
        _seasonService = seasonService;
    }

    public async Task Handle(DeleteSeasonCommand request, CancellationToken cancellationToken)
    {
        await _seasonService.Delete(request.Id, cancellationToken);
    }
}
