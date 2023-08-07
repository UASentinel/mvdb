using MediatR;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Medias.Commands.Update;

public class UpdateMediaCommandHandler : IRequestHandler<UpdateMediaCommand>
{
    private readonly IMediaService _mediaService;

    public UpdateMediaCommandHandler(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    public async Task Handle(UpdateMediaCommand request, CancellationToken cancellationToken)
    {
        var media = new Media()
        {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            TrailerLink = request.TrailerLink,
            MediaType = request.MediaType,
            AgeRatingId = request.AgeRatingId,
            Duration = request.Duration,
            ReleaseDate = request.ReleaseDate
        };

        await _mediaService.Update(media, request.PosterFile, cancellationToken);
    }
}
