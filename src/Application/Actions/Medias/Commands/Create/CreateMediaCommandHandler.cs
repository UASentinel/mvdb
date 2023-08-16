using MediatR;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Medias.Commands.Create;

public class CreateMediaCommandHandler : IRequestHandler<CreateMediaCommand, int>
{
    private readonly IMediaService _mediaService;

    public CreateMediaCommandHandler(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    public async Task<int> Handle(CreateMediaCommand request, CancellationToken cancellationToken)
    {
        var media = new Media()
        {
            Title = request.Title,
            Description = request.Description,
            TrailerLink = request.TrailerLink,
            MediaType = request.MediaType,
            AgeRatingId = request.AgeRatingId,
            Duration = request.Duration,
            ReleaseDate = request.ReleaseDate
        };

        await _mediaService.Create(media, request.PosterFile, cancellationToken);

        return media.Id;
    }
}
