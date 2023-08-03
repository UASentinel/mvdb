using MediatR;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Genres.Queries.GetById;

public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, GenreDto>
{
    private readonly IGenreService _genreService;

    public GetGenreByIdQueryHandler(IGenreService genreService)
    {
        _genreService = genreService;
    }

    public async Task<GenreDto> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        var genre = await _genreService.GetById(request.Id);

        if (genre == null)
            return null;

        var genreDto = new GenreDto()
        {
            Id = genre.Id,
            Name = genre.Name
        };
        return genreDto;
    }
}
