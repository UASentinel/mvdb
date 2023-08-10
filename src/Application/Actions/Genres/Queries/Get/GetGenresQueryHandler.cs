using MediatR;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Actions.Medias.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Genres.Queries.Get;

public class GetGenresCommandHandler : IRequestHandler<GetGenresQuery, ICollection<GenreDto>>
{
    private readonly IGenreService _genreService;

    public GetGenresCommandHandler(IGenreService genreService)
    {
        _genreService = genreService;
    }

    public async Task<ICollection<GenreDto>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {
        var genres = _genreService.Get();

        if (genres == null)
            return new List<GenreDto>();

        var genreDtos = new List<GenreDto>();
        foreach (var genre in genres)
        {
            var genreDto = new GenreDto()
            {
                Id = genre.Id,
                Name = genre.Name
            };
            genreDtos.Add(genreDto);
        }

        return genreDtos;
    }
}
