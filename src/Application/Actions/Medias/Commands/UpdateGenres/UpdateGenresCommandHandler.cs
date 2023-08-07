using MediatR;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Application.Services;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Medias.Commands.AddGenres;

public class UpdateGenresCommandHandler : IRequestHandler<UpdateGenresCommand>
{
    private readonly IMediaService _mediaService;

    public UpdateGenresCommandHandler(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    public async Task Handle(UpdateGenresCommand request, CancellationToken cancellationToken)
    {
        var mediaGenres = new List<MediaGenre>();
        foreach(var mediaGenreDto in request.MediaGenreDtos)
        {
            mediaGenres.Add(new MediaGenre()
            {
                MediaId = request.MediaId,
                GenreId = mediaGenreDto.GenreId,
                Order = mediaGenreDto.Order
            });
        }

        await _mediaService.UpdateGenres(request.MediaId, mediaGenres, cancellationToken);
    }
}
