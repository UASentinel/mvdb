using MediatR;
using MvDb.Application.Common.Interfaces.EntityServices;

namespace MvDb.Application.Actions.Genres.Commands.Delete;

public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand>
{
    private readonly IGenreService _genreService;

    public DeleteGenreCommandHandler(IGenreService genreService)
    {
        _genreService = genreService;
    }

    public async Task Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        await _genreService.Delete(request.Id, cancellationToken);
    }
}
