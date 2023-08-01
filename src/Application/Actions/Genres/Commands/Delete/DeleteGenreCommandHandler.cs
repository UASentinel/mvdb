using MediatR;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

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
