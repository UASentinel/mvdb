using Microsoft.AspNetCore.Mvc;
using MvDb.Application.Actions.AgeRatings.Commands.Create;
using MvDb.Application.Actions.AgeRatings.Commands.Delete;
using MvDb.Application.Actions.AgeRatings.Commands.Update;
using MvDb.Application.Actions.AgeRatings.DataTransferObjects;
using MvDb.Application.Actions.AgeRatings.Queries.Get;
using MvDb.Application.Actions.AgeRatings.Queries.GetById;
using Microsoft.AspNetCore.Authorization;

namespace MvDb.WebUI.Controllers;

[Authorize(Roles = "Administrator")]
public class AgeRatingsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ICollection<AgeRatingDto>>> Get()
    {
        var genres = await Mediator.Send(new GetAgeRatingsQuery());

        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AgeRatingDto>> Get(int id)
    {
        return await Mediator.Send(new GetAgeRatingByIdQuery(id));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateAgeRatingCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, UpdateAgeRatingCommand command)
    {
        if (id != command.AgeRatingId)
            return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteAgeRatingCommand(id));

        return NoContent();
    }
}
