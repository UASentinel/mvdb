using MediatR;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Genres.Commands.Update;

public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand>
{
    private readonly IGenreService _genreService;

    public UpdateGenreCommandHandler(IGenreService genreService)
    {
        _genreService = genreService;
    }

    public async Task Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = new Genre()
        {
            Id = request.GenreId,
            Name = request.Name
        };

        await _genreService.Update(genre, cancellationToken);
    }
}
