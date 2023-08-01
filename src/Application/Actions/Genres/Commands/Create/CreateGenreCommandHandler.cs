using MediatR;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Genres.Commands.Create;

public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, int>
{
    private readonly IGenreService _genreService;

    public CreateGenreCommandHandler(IGenreService genreService)
    {
        _genreService = genreService;
    }

    public async Task<int> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = new Genre(name: request.Name);

        await _genreService.Create(genre, cancellationToken);

        return genre.Id;
    }
}
