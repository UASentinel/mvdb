using MediatR;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Domain.Entities;

namespace MvDb.Application.Actions.Medias.Commands.UpdateSeasonsOrder;

public class UpdateSeasonsOrderCommandHandler : IRequestHandler<UpdateSeasonsOrderCommand>
{
    private readonly ISeasonService _seasonService;

    public UpdateSeasonsOrderCommandHandler(ISeasonService seasonService)
    {
        _seasonService = seasonService;
    }

    public async Task Handle(UpdateSeasonsOrderCommand request, CancellationToken cancellationToken)
    {
        var mediaSeasons = new List<Season>();
        foreach (var mediaSeasonDto in request.MediaSeasonDtos)
        {
            mediaSeasons.Add(new Season()
            {
                Id = mediaSeasonDto.SeasonId,
                Order = mediaSeasonDto.Order
            });
        }

        await _seasonService.UpdateSeasonsOrder(mediaSeasons, cancellationToken);
    }
}
